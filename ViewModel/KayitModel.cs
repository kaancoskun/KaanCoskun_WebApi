using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaanPrj.ViewModel
{
    public class KayitModel
    {
        public string kayitId { get; set; }
        public string kayitKatId { get; set; }
        public string kayitUyeId { get; set; }
        public UyeModel uyeBilgi { get; set; }
        public KatagoriModel katBilgi { get; set; }


    }
}