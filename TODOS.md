# TODOS

Deferred items from reviews. Not blocking current work.

---

## JWT ValidateIssuer/ValidateAudience Hardening

**What:** `Infrastructure/Registration.cs:35-36` — `ValidateIssuer = false` ve `ValidateAudience = false`. Her geçerli imzalı token, hangi issuer'dan gelirse gelsin kabul ediliyor.

**Why:** T1 (UseAuthentication aktif edilince) JWT doğrulaması başlar ama issuer/audience kontrolü devre dışı. İç sistem için signing key doğrulaması yeterli kabul edildi. API'nin dışarıya açılması durumunda risk artar.

**Pros:** Güvenlik sertleştirilmesi; deployment topology'ye güvenmek yerine token claim'lerini doğrulama.

**Cons:** Mevcut tokenlerin Issuer/Audience claim'leri yoksa veya yanlışsa, tüm kullanıcılar yeniden login olmak zorunda kalır.

**Context:** Şu an iç ağda (aundeteknik.com). Reverse proxy veya VPN split-tunnel ile API dışarıya açılırsa herhangi bir geçerli imzalı token kabul edilir.

**Depends on:** T1 tamamlanmış olmalı.

---

## ApiClientService Token Refresh Locking (SemaphoreSlim)

**What:** `WebUI/Services/ApiClientService.cs:GetHttpClientWithTokenAsync()` — token expire olduğunda eşzamanlı 7 Populate çağrısı aynı anda RefreshTokenAsync() çağırabilir.

**Why:** T3'te Task.WhenAll ekleniyor. Token geçerliyken tamamen güvenli. Token expire olduğu anda (20 dakika session sonunda) 7 paralel refresh denemesi oluşabilir. Rotating refresh token varsa bazı çağrılar başarısız olabilir.

**Pros:** `SemaphoreSlim(1,1)` ile ilk refresh kazanır, diğerleri bekler → yeni token kullanır. Race condition tamamen çözülür.

**Cons:** Küçük latency eklenir (lock overhead). Tek admin kullanıcı senaryosunda nadiren tetiklenir.

**Context:** Risk T3 kararında kabul edildi. İç sistem, düşük eşzamanlılık. Geçici yorum satırı: `// TODO: add SemaphoreSlim if rotating refresh tokens cause parallel-refresh failures`

**Depends on:** T2 (AddScoped) ve T3 (Task.WhenAll) tamamlanmış olmalı.
