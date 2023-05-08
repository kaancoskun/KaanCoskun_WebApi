using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KaanPrj.Models;
using KaanPrj.ViewModel;

namespace KaanPrj.Controllers
{
    public class ServisController : ApiController
    {
        DB01Entities db = new DB01Entities();
        SonucModel sonuc = new SonucModel();

        #region Katagori


        [HttpGet]
        [Route("api/katagoriliste")]
        public List<KatagoriModel> KatagoriListe()
        {
            List<KatagoriModel> liste = db.Katagori.Select(x=>new KatagoriModel() 
            {
                katId = x.katId,
                katKodu = x.katKodu,
                katAdi = x.katAdi

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/katbyid/{katId}")]

        public KatagoriModel KatById(string katId)
        {
            KatagoriModel kayit = db.Katagori.Where(s => s.katId == katId).Select
                (x=>new KatagoriModel()
                {
                    katId = x.katId,
                    katKodu = x.katKodu,
                    katAdi = x.katAdi

                }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/katagoriekle")]

        public SonucModel KatagoriEkle(KatagoriModel model)
        {
            if (db.Katagori.Count(s => s.katKodu == model.katKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Katagori Kodu Kayıtlıdır!";
                return sonuc;
            }

            Katagori yeni = new Katagori();
            yeni.katId = Guid.NewGuid().ToString();
            yeni.katKodu = model.katKodu;
            yeni.katAdi = model.katAdi;
            db.Katagori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Katagori Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/katagoridüzenle")]

        public SonucModel KatagoriDuzenle(KatagoriModel model)
        {
            Katagori kayit = db.Katagori.Where(s=>s.katId==model.katId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Katagori Bulunamadı!";
                return sonuc;

            }

            kayit.katKodu = model.katKodu;
            kayit.katAdi = model.katAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Katagori Düzenlendi";

            return sonuc;

        }
        [HttpDelete]
        [Route("api/katagorisil/{katId}")]

        public SonucModel KatagoriSil(string katId)
        {

            Katagori kayit = db.Katagori.Where(s => s.katId == katId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Katagori Bulunamadı!";
                return sonuc;

            }

            if (db.Kayit.Count(s=>s.kayitKatId==katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Katagoriye Kayıtlı Uye Olduğu Içın Katagori Silinemez!";
                return sonuc;
            }


            db.Katagori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Katagori Silindi";
            return sonuc;
        }
        #endregion

        #region Uye

        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(x=>new UyeModel() 
            {
                uyeId=x.uyeId,
                uyeNo=x.uyeNo,
                uyeAdsoyad=x.uyeAdsoyad,
                uyeDogTarih=x.uyeDogTarih,
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(string uyeId)
        {
            UyeModel kayit = db.Uye.Where(s=>s.uyeId==uyeId).Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                uyeNo = x.uyeNo,
                uyeAdsoyad = x.uyeAdsoyad,
                uyeDogTarih = x.uyeDogTarih,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s=>s.uyeNo==model.uyeNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Uye Numarası Kayıtlıdır!";
            }

            Uye yeni = new Uye();
            yeni.uyeId = Guid.NewGuid().ToString();
            yeni.uyeNo = model.uyeNo;
            yeni.uyeAdsoyad = model.uyeAdsoyad;
            yeni.uyeDogTarih = model.uyeDogTarih;
            db.Uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Uye Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == model.uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Uye Bulunamadı";
                return sonuc;
            }
            kayit.uyeNo = model.uyeNo;
            kayit.uyeAdsoyad = model.uyeAdsoyad;
            kayit.uyeDogTarih = model.uyeDogTarih;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Uye Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]

        public SonucModel UyeSil(string uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Uye Bulunamadı";
                return sonuc;
            }

            if (db.Kayit.Count(s=>s.kayitUyeId==uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Uye Üzerinde Katagori Kaydı Olduğu İçin Uye Silinemez!";
                return sonuc;
            }


            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Uye Silindi";
            return sonuc;
        }
        #endregion

        [HttpGet]
        [Route("api/uyekatagoriliste/{uyeId}")]
        public List<KayitModel> UyeKatagoriListe(string uyeId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitUyeId == uyeId).Select(x => new KayitModel() 
            {
                kayitId=x.kayitId,
                kayitKatId=x.kayitKatId,
                kayitUyeId=x.kayitUyeId,

            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.uyeBilgi = UyeById(kayit.kayitUyeId);
                kayit.katBilgi = KatById(kayit.kayitKatId);
            }



            return liste;
        }
        [HttpGet]
        [Route("api/katagoriuyeliste/{katId}")]
        public List<KayitModel> KatagoriUyeListe(string katId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitKatId == katId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitKatId = x.kayitKatId,
                kayitUyeId = x.kayitUyeId,

            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.uyeBilgi = UyeById(kayit.kayitUyeId);
                kayit.katBilgi = KatById(kayit.kayitKatId);
            }



            return liste;
        }

        [HttpPost]
        [Route("api/kayitekle")]
        public SonucModel KayitEkle(KayitModel model)
        {
            if (db.Kayit.Count(s=>s.kayitKatId==model.kayitKatId && s.kayitUyeId==model.kayitUyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ilgili Uye Katagoriye Önceden Kayıtlıdır!";
                return sonuc;
            }

            Kayit yeni = new Kayit();
            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayitUyeId = model.kayitUyeId;
            yeni.kayitKatId = model.kayitKatId;
            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Katagori Kaydı Eklendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kaiytsil/{kayitId}")]
        public SonucModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;

            }
            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Katagori Kaydı Silindi";

            return sonuc;   
        }


    }


}
