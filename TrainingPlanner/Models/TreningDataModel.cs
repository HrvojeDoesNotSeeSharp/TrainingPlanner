using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Models
{
    public class TreningDataModel
    {
        /*Clan tablica*/
        [HiddenInput]
        public int ClanId { get; set; }

        public string ClanIme { get; set; }
        public string ClanPrezime { get; set; }


        /*Trening tablica*/
        [HiddenInput]
        public int TreningId { get; set; }

        public string TreningImeTreninga { get; set; }
        public System.DateTime TreningDatum { get; set; }
        public string TreningTip { get; set; }
        public Nullable<short> TreningBrojKrugova { get; set; }
        
        
        /*Zagrijavanje tablica*/
        public List<Zagrijavanje> ListaZagrijavanja { get; set; }

        /*Vjezbe tablica*/
        public List<Vjezba> ListaVjezbi { get; set; }
        

        /*Istezanje tablica*/
        public List<Istezanje> ListaIstezanja { get; set; }

        public string Napomena { get; set; }
    }
}