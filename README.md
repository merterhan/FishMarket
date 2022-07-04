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
- Encryption, Decryption ve Hashing işlemleri IUitility'den implement edilen UtilityManager classında yapılmıştır.
- .net 6'da moq'ların içerisinde dependency injection yönetiminde sorun yaşadığım için unit testler service katmanındaki bazı metotlar için yazılmıştır. Ayrıca bir entegrasyon testi yazılmamıştır. 

Notlar:
- code first oluşturulan tabloların konfigürasyonları kolay yönetilebilmesi için DataAccess/Concrete/EntityFrameworkCore/Configurations klasörü altında ayrılmıştır ve bu şekilde migration çıkılmıştır. Swagger şema konfigürasyonu hatası aldığından dolayı tekrar DbContext içerisine alınıp proje çalıştırılmıştır.
- tablo konfigürasyonları fluent kullanılarak düzenlenmesine rağmen, foreign key ilişkileri mysql sunucusunda alınan ilişki yetkilendirme sorunundan dolayı oluşmamıştır. tablolardaki diğer unique key ve primary key'ler oluşmasında bir sorun olmamıştır.
- Web projesinde post gönderimleri için front-end kısmında genel örnek olması açısından hem razor tag-helper'ları hem de jquery ajax gönderimleri yapılmıştır.

Genel:
Proje n-Tier Architect ile olşuturulmuştur. EntityFrameworkCore 6.0.6 kullanılmıştır. Web projesinden api'ye direk erişim sağlanmamış olup Refit kullanılarak bir rest client yazılmıştır (FishMarket.Client). Gösterim açısından hard-coded alanlar şifrelenip appsettings.json dosyasında tutulmuştur. Bazı class'larda hıslı geliştirme açısından bazı katmanlarda hardcoded veriler ayrı bir settings dosyasına taşınmamıştır. Hata loglaması örnekleri api projesinde FishMarketController'da mevcuttur.
