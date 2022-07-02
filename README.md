# FishMarket
.NET 6

Proje Dizini:
-/src
  --/common
    ---client
    ---dto
  --/core
    ---core
    ---dataaccess
    ---entities
  --/integration
    ---api
  --presentation
    ---web
  --/tests
    ---test

Önemli:
- swagger'da test etmek için bearer authorizaton gerekmektedir. Bunun için gerekli JWT Token'ı /GetToken metodundan sağlayabilirsiniz.
- swagger şema sorunundan dolayı tüm metotlarda kilit işareti(authorizaiton) görünmektedir fakat GetToken,Login,Register ve Balık listeleme metodları public olarak erişilebilirdir.
- Yeni kullanıcı kaydederken result olarak email doğrulama linki döndürülmektedir. Email doğrulandıktan sonra login olunabilir. Dönen linke tıklayarak email doğrulanabilir. Bunun için ayrıca bir EmailClient oluşturulup mail gönderimi yapılmamıştır.
- Veri Tabanı DB First oluşturulmuş olup remote bir MYSQL server üzerinde oluşturulmuştur. Projeyi çalıştıracak kişinin Migration için IP adresi izinleri sağlanması gerekmektedir.

Genel:
Proje n-Tier Architect ile olşuturulmuştur. EntityFrameworkCore 6.0.6 kullanılmıştır. Web projesinden api'ye direk erişim sağlanmamış olup Refit kullanılarak bir rest client sağlanmıştır. Gösterim açısından Hardcoded alanlar şifrelenip appsettings dosyasında tutulmuştur. Bazı class'larda Hızlı geliştirme açısından bazı katmanlarda hardcoded veriler ayrı bir settings dosyasına taşınmamıştır. Hata loglaması örnekleri api projesinde FishMarketController'da mevcuttur.
