# FishMarket

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
- Proje .net 6 ile yazıldığından visual studio 2022 kurulumu gerektirmektedir.
- Projeyi çalıştırmak için api ve web projeleri multiple startup olarak işaretlenmelidir.
- Veri Tabanı DB First oluşturulmuş olup remote bir MYSQL server üzerinde oluşturulmuştur. Projeyi çalıştıracak kişinin Migration için IP adresi izinleri sağlanması gerekmektedir. migraiton için dataaccess katmanını set as startup projesi olarak işaretlemeniz gerekmektedir.
- swagger'da test etmek için bearer authorizaton gerekmektedir. Bunun için gerekli JWT Token'ı /GetToken metodundan sağlayabilirsiniz. Swagger'daki authorization için güvenlik şeması tipi http olarak oluşturulmuştur. Token'ı direk yapıştırıp (Bearer yazmadan) kullanabilirsiniz.
- swagger şema sorunundan dolayı tüm metotlarda kilit işareti(authorizaiton) görünmektedir fakat GetToken,Login,Register ve Balık listeleme metodları public olarak erişilebilirdir.
- Yeni kullanıcı kaydederken result olarak email doğrulama linki döndürülmektedir. Email doğrulandıktan sonra login olunabilir. Dönen linke tıklayarak email doğrulanabilir. Bunun için ayrıca bir EmailClient oluşturulup mail gönderimi yapılmamıştır.
- service katmanındaki classların yaşam süreleri scoped olarak ayarlanmıştır. Map işlemleri için AutoMapper kullanılmıştır ve lifecycle'u singleton'dır.

Genel:
Proje n-Tier Architect ile olşuturulmuştur. EntityFrameworkCore 6.0.6 kullanılmıştır. Web projesinden api'ye direk erişim sağlanmamış olup Refit kullanılarak bir rest client sağlanmıştır. Gösterim açısından Hardcoded alanlar şifrelenip appsettings dosyasında tutulmuştur. Bazı class'larda Hızlı geliştirme açısından bazı katmanlarda hardcoded veriler ayrı bir settings dosyasına taşınmamıştır. Hata loglaması örnekleri api projesinde FishMarketController'da mevcuttur.
