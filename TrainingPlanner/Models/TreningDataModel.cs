using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TrainingPlanner.Models
{
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(TreningDataModelMetadata))]
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
        public DateTime TreningDatum { get; set; }
        public string TreningTip { get; set; }

        public string NapomenaZagrijavanje { get; set; }
        public string NapomenaVjezba { get; set; }
        public string NapomenaIstezanje { get; set; }

        /*Sekcija popisa vjezbi*/
        public List<SekcijaVjezbi> SekcijaVjezbi { get; set; }

        /*Istezanje tablica*/
        public List<Istezanje> ListaIstezanja { get; set; }

        /*ZagrijavanjeSkupina tablica*/
        public List<ZagrijavanjeSkupina> ZagrijavanjeSkupina { get; set; }

        public string Napomena { get; set; }
    }
}