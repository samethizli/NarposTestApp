using NarposTestApp;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V138.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using DevToolsSessionDomains = OpenQA.Selenium.DevTools.V138.DevToolsSessionDomains;

public class NetworkLogger
{
    private ChromeDriver driver;
    private DevToolsSession devToolsSession;
    private DevToolsSessionDomains devTools;
    private ConcurrentDictionary<string, string> requestMethods = new ConcurrentDictionary<string, string>();
    private ConcurrentDictionary<string, string> requestPayload = new ConcurrentDictionary<string, string>();
    private ConcurrentDictionary<string, string> responseStatusTexts = new ConcurrentDictionary<string, string>();
    private ConcurrentDictionary<string, ResponseInfo> responseMap = new ConcurrentDictionary<string, ResponseInfo>();
    public event Action<string, string, int, string, string, string, string> ResponseLogged;

    public NetworkLogger()
    {
        driver = SharedState.Driver ?? throw new Exception("Driver henüz başlatılmadı.");
    }

    public async Task StartAsync()
    {
        devToolsSession = driver.GetDevToolsSession();

        devTools = devToolsSession.GetVersionSpecificDomains<DevToolsSessionDomains>();

        await devTools.Network.Enable(new EnableCommandSettings());

        await Task.Delay(200);

        devTools.Network.RequestWillBeSent += (sender, e) =>
        {
            var requestId = e.RequestId;
            var method = e.Request.Method;
            var payload = e.Request.PostData;
            requestMethods[requestId] = method;
            requestPayload[requestId] = payload;



        };
        await Task.Delay(200);

        devTools.Network.ResponseReceived += (sender, e) =>
        {
            var requestId = e.RequestId;
            var url = e.Response.Url;
            var statusCode = (int)e.Response.Status;
            var statusTextRaw = e.Response.StatusText;
            var time = DateTime.Now.ToString("HH:mm:ss");

            string method = requestMethods.TryGetValue(requestId, out var m) ? m : "UNKNOWN";
            string payload = requestPayload.TryGetValue(requestId, out var p) ? p : null;
            string statusDescription = GetStatusDescription(statusCode);

            responseMap[requestId] = new ResponseInfo
            {
                Url = url,
                StatusCode = statusCode,
                StatusDescription = statusDescription,
                StatusText = statusTextRaw,
                Method = method,
                Payload = payload,
                Time = time
            };
        };


        await Task.Delay(200);

        devTools.Network.LoadingFinished += async (sender, e) =>
        {
            var requestId = e.RequestId;
            await Task.Delay(200);

            ResponseInfo info = null;
            for (int i = 0; i < 5; i++)
            {
                if (!responseMap.TryGetValue(requestId, out info))
                    break;

                await Task.Delay(200);

            }


            string responseBody;

            try
            {
                var bodyResult = await devToolsSession.SendCommand<
                    GetResponseBodyCommandSettings,
                    GetResponseBodyCommandResponse>(
                    new GetResponseBodyCommandSettings { RequestId = requestId });

                responseBody = bodyResult.Body;
            }
            catch
            {
                responseBody = "[Body alınamadı]";
            }
            await Task.Delay(100);

            ResponseLogged?.Invoke(
                info.Url,
                info.Method,
                info.StatusCode,
                info.StatusDescription,
                info.Payload,
                responseBody,
                info.Time
            );

            // Temizle
            responseMap.TryRemove(requestId, out _);
        };


        


    }
    private string GetStatusDescription(int statusCode)
    {
        if (statusCode < 399 && statusCode >= 200)
        {
            return "BAŞARILI";
        }
        else
        {
            return "BAŞARISIZ";
        }
        //switch (statusCode)
        //{
        //    case 100: return "Devam";
        //    case 101: return "Protokoller Değiştiriliyor";
        //    case 102: return "İşleniyor";
        //    case 200: return "BAŞARILI";
        //    case 201: return "Oluşturuldu";
        //    case 202: return "Kabul Edildi";
        //    case 203: return "Yetkisiz Bilgi";
        //    case 204: return "İçerik Yok";
        //    case 205: return "İçerik Sıfırlandı";
        //    case 206: return "Kısmi İçerik";
        //    case 300: return "Çoklu Seçenek";
        //    case 301: return "Kalıcı Olarak Taşındı";
        //    case 302: return "Bulundu";
        //    case 303: return "Başka Yerde Görüntüle";
        //    case 304: return "Değişmedi";
        //    case 305: return "Proxy Kullan";
        //    case 307: return "Geçici Yönlendirme";
        //    case 308: return "Kalıcı Yönlendirme";
        //    case 400: return "Hatalı İstek";
        //    case 401: return "Yetkilendirme Gerekiyor";
        //    case 402: return "Ödeme Gerekiyor";
        //    case 403: return "Yasaklandı";
        //    case 404: return "Bulunamadı";
        //    case 405: return "İzin Verilmeyen Metot";
        //    case 406: return "Kabul Edilemez";
        //    case 407: return "Proxy Kimlik Doğrulaması Gerekiyor";
        //    case 408: return "İstek Zaman Aşımına Uğradı";
        //    case 409: return "Çakışma";
        //    case 410: return "Gitmiş";
        //    case 411: return "Uzunluk Gerekiyor";
        //    case 412: return "Önkoşul Başarısız";
        //    case 413: return "İçerik Çok Büyük";
        //    case 414: return "URI Çok Uzun";
        //    case 415: return "Desteklenmeyen Medya Türü";
        //    case 416: return "İstek Aralığı Uygun Değil";
        //    case 417: return "Beklenti Başarısız";
        //    case 426: return "Yükseltme Gerekiyor";
        //    case 500: return "Sunucu Hatası";
        //    case 501: return "Yapılmamış";
        //    case 502: return "Kötü Ağ Geçidi";
        //    case 503: return "Hizmet Kullanılamıyor";
        //    case 504: return "Ağ Geçidi Zaman Aşımı";
        //    case 505: return "HTTP Versiyonu Desteklenmiyor";
        //    default: return "Bilinmeyen Durum";
        //}
    }


}
