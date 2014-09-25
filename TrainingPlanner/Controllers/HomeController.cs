using TrainingPlanner;
using TrainingPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Reflection;

namespace TrainingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private TreningModelContainer context = new TreningModelContainer();

    
        public ActionResult Index()
        {
            List<Clan> lista = context.Clan.ToList();
            return View(lista);
        }

        /****************Akcije sa zagrijavanjem, vjezbama i istezanjem****************/
        [HttpGet]
        public ActionResult ZagrijavanjePopis()
        {
            List<ZagrijavanjePopis> listaZagrijavanja = new List<ZagrijavanjePopis>();
            listaZagrijavanja = context.ZagrijavanjePopis.ToList();
            if (listaZagrijavanja.Count == 0)
            {
                listaZagrijavanja = new List<ZagrijavanjePopis>(){new ZagrijavanjePopis{ ZagrijavanjeId = 0, Info="", Naziv=""}};
                return View(listaZagrijavanja);
            }
            else
            {
                return View(listaZagrijavanja);
            }
            
        }

        public ActionResult IzbrisiZagrijavanje(int id=0)
        {
            ZagrijavanjePopis zp = context.ZagrijavanjePopis.Find(id);
            context.ZagrijavanjePopis.Remove(zp);
            context.SaveChanges();
            return RedirectToAction("ZagrijavanjePopis", "Home");
        }

        public ActionResult DetaljiZagrijavanja(int id = 0)
        {
            ZagrijavanjePopis zp = context.ZagrijavanjePopis.Find(id);
            return View(zp);
        }

        [HttpGet]
        public ActionResult DodajNovoZagrijavanje()
        {
            ZagrijavanjePopis zp = new ZagrijavanjePopis();
            return View(zp);
        }

        [HttpPost]
        public ActionResult DodajNovoZagrijavanje(ZagrijavanjePopis zp)
        {
            context.ZagrijavanjePopis.Add(zp);
            context.SaveChanges();
            return RedirectToAction("ZagrijavanjePopis", "Home");
        }

        [HttpGet]
        public ActionResult IzmijeniZagrijavanje(int id = 0)
        {
            ZagrijavanjePopis zp = context.ZagrijavanjePopis.Find(id);
            return View(zp);
        }

        [HttpPost]
        public ActionResult IzmijeniZagrijavanje(ZagrijavanjePopis zp)
        {
            context.Entry(zp).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("ZagrijavanjePopis", "Home");
        }

        [HttpGet]
        public ActionResult VjezbePopis()
        {
            List<VjezbePopis> listaVjezbi = new List<VjezbePopis>();
            listaVjezbi = context.VjezbePopis.ToList();
            if (listaVjezbi.Count == 0)
            {
                listaVjezbi = new List<VjezbePopis>() { new VjezbePopis { VjezbeId = 0, ImeVjezbe = "", Info="", Slika=null } };
                return View(listaVjezbi);
            }
            else
            {
                return View(listaVjezbi);
            }
        }

        public ActionResult IzbrisiVjezbu(int id = 0)
        {
            VjezbePopis vjp = context.VjezbePopis.Find(id);
            context.VjezbePopis.Remove(vjp);
            context.SaveChanges();
            return RedirectToAction("VjezbePopis", "Home");
        }

        public ActionResult DetaljiVjezbe(int id = 0)
        {
            VjezbePopis vjp = context.VjezbePopis.Find(id);
            return View(vjp);
        }

        [HttpGet]
        public ActionResult DodajNovuVjezbu()
        {
            VjezbePopis vjp = new VjezbePopis();
            return View(vjp);
        }

        [HttpPost]
        public ActionResult DodajNovuVjezbu(VjezbePopis vjp)
        {
            context.VjezbePopis.Add(vjp);
            context.SaveChanges();
            return RedirectToAction("VjezbePopis", "Home");
        }

        [HttpGet]
        public ActionResult IzmijeniVjezbu(int id = 0)
        {
            VjezbePopis vjp = context.VjezbePopis.Find(id);
            return View(vjp);
        }

        [HttpPost]
        public ActionResult IzmijeniVjezbu(VjezbePopis vjp)
        {
            context.Entry(vjp).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("VjezbePopis", "Home");
        }

        [HttpGet]
        public ActionResult IstezanjePopis()
        {
            List<IstezanjePopis> listaIstezanja = new List<IstezanjePopis>();
            listaIstezanja = context.IstezanjePopis.ToList();
            if (listaIstezanja.Count == 0)
            {
                listaIstezanja = new List<IstezanjePopis>() { new IstezanjePopis { IstezanjeId=0, Naziv="", Info="" } };
                return View(listaIstezanja);
            }
            else
            {
                return View(listaIstezanja);
            }
        }

        public ActionResult IzbrisiIstezanje(int id = 0)
        {
            IstezanjePopis ip = context.IstezanjePopis.Find(id);
            context.IstezanjePopis.Remove(ip);
            context.SaveChanges();
            return RedirectToAction("IstezanjePopis", "Home");
        }

        public ActionResult DetaljiIstezanja(int id = 0)
        {
            IstezanjePopis ip = context.IstezanjePopis.Find(id);
            return View(ip);
        }

        [HttpGet]
        public ActionResult DodajNovoIstezanje()
        {
            IstezanjePopis ip = new IstezanjePopis();
            return View(ip);
        }

        [HttpPost]
        public ActionResult DodajNovoIstezanje(IstezanjePopis ip)
        {
            context.IstezanjePopis.Add(ip);
            context.SaveChanges();
            return RedirectToAction("IstezanjePopis", "Home");
        }

        [HttpGet]
        public ActionResult IzmijeniIstezanje(int id = 0)
        {
            IstezanjePopis ip = context.IstezanjePopis.Find(id);
            return View(ip);
        }

        [HttpPost]
        public ActionResult IzmijeniIstezanje(IstezanjePopis ip)
        {
            context.Entry(ip).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("IstezanjePopis", "Home");
        }

        /****************Akcije sa clanovima i testovima****************/

        [HttpGet]
        public ActionResult DodajClana()
        {      
            Clan c = new Clan();
            return View(c);
        }

        [HttpPost]
        public ActionResult DodajClana(Clan c)
        {
            short age = (short)((DateTime.Now - c.GodinaRodenja).TotalDays / 365.242199);
            c.GodineStarosti = age;
            context.Clan.Add(c);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult IzbrisiClana(int id)
        {
            Clan c = context.Clan.Find(id);
            
            var query = from x in context.Trening
                        where x.ClanId == id
                        select x;

            if (query.FirstOrDefault() != null)
            {
                foreach (Trening a in query.ToList())
                {
                var query1 = from x in context.Trening
                             join z in context.Zagrijavanje on x.TreningId equals z.TreningId
                             where x.TreningId == a.TreningId
                             select z;

                var query2 = from x in context.Trening
                             join v in context.Vjezba on x.TreningId equals v.TreningId
                             where x.TreningId == a.TreningId
                             select v;

                var query3 = from x in context.Trening
                             join i in context.Istezanje on x.TreningId equals i.TreningId
                             where x.TreningId == a.TreningId
                             select i;

                foreach (Zagrijavanje b in query1.ToList())
                {
                    context.Zagrijavanje.Remove(b);
                }

                foreach (Vjezba b in query2.ToList())
                {
                    context.Vjezba.Remove(b);
                }

                foreach (Istezanje b in query3.ToList())
                {
                    context.Istezanje.Remove(b);
                }
                
                    context.Trening.Remove(a);
                }

            }

            var queryTest = from x in context.Test
                            where x.ClanId == id
                            select x;

            if (queryTest.FirstOrDefault() != null)
            {
                foreach (Test a in queryTest.ToList())
                {
                    context.Test.Remove(a);
                }
            }

            context.Clan.Remove(c);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult IzmijeniClana(int id = 0)
        {
            Clan c = context.Clan.Find(id);
            return View(c);
        }

        [HttpPost]
        public ActionResult IzmijeniClana(Clan c)
        {
            short age = (short)((DateTime.Now - c.GodinaRodenja).TotalDays / 365.242199);
            c.GodineStarosti = age;
            context.Entry(c).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DetaljiClana(int id = 0)
        {
            Clan c = context.Clan.Find(id);

            var queryTest = from x in context.Test
                            where x.ClanId == id
                            select x;

            ClanTestModel ctm = new ClanTestModel() {Clan = c, listaTest = queryTest.ToList() };

            return View(ctm);
        }

        [HttpGet]
        public ActionResult Test(int id = 0)
        {
            var query = from x in context.Test
                        where x.ClanId == id
                        select x;

            var query1 = from x in context.Clan
                         where x.ClanId == id
                         select x;

            Clan c = query1.Single();

            TestList listaT = new TestList() {clan = c };
            
            if (query.FirstOrDefault() != null)
            {
                listaT.listaTestova = query.ToList();
            };

            return View(listaT);
        }

        [HttpGet]
        public ActionResult DodajTest(int id = 0)
        {
            Test t = new Test() {ClanId = id };
            return View(t);
        }

        [HttpPost]
        public ActionResult DodajTest(Test t, HttpPostedFileBase dijagnostika1)
        {

            if (dijagnostika1 != null)
            {
                t.DijagnostikaType = dijagnostika1.ContentType;
                
                t.Dijagnostika = new byte[dijagnostika1.ContentLength];
                dijagnostika1.InputStream.Read(t.Dijagnostika, 0, dijagnostika1.ContentLength);

            }
 
            context.Test.Add(t);
            context.SaveChanges();

            return RedirectToAction("Test", new {id = t.ClanId });
        }

        public FileContentResult GetImage(int TestId)
        {
            Test t = context.Test.Find(TestId);

            if (t != null)
            {
                return File(t.Dijagnostika, t.DijagnostikaType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult IzbrisiTest(int id)
        {
            Test t = context.Test.Find(id);

            context.Test.Remove(t);
            context.SaveChanges();

            return RedirectToAction("Test", new { id = t.ClanId });
            
        }

        [HttpGet]
        public ActionResult IzmijeniTest(int id = 0)
        {
            Test t = context.Test.Find(id);
            return View(t);
        }

        [HttpPost]
        public ActionResult IzmijeniTest(Test t, HttpPostedFileBase dijagnostika)
        {
            if (dijagnostika != null)
            {
                t.DijagnostikaType = dijagnostika.ContentType;
                t.Dijagnostika = new byte[dijagnostika.ContentLength];
                dijagnostika.InputStream.Read(t.Dijagnostika, 0, dijagnostika.ContentLength);
            }

            context.Entry(t).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("Test", new { id = t.ClanId });
        }

        public ActionResult DetaljiTest(int id)
        {
            Test t = context.Test.Find(id);
            return View(t);

        }


        /*****************Akcije sa treningom*****************/

        [HttpGet]
        public ActionResult TreningPopis(int id = 0)
        {
            var query1 = from x in context.Clan
                         where x.ClanId == id
                         select x;

            Clan c = (Clan)query1.Single();

            ViewData["ImeClana"] = c.Ime;
            ViewData["PrezimeClana"] = c.Prezime;

            var query = from x in context.Trening
                        join y in context.Clan on x.ClanId equals y.ClanId
                        where y.ClanId == id
                        orderby x.DatumTreninga ascending
                        select x;

            foreach (Trening tr in query.ToList())
	        {
		        if(tr.ImeTreninga == null)
                {
                    var query4 = from x in context.Trening
                         join z in context.Zagrijavanje on x.TreningId equals z.TreningId
                         where x.TreningId == tr.TreningId
                         select z;

                    var query2 = from x in context.Trening
                         join v in context.Vjezba on x.TreningId equals v.TreningId
                         where x.TreningId == tr.TreningId
                         select v;

                    var query3 = from x in context.Trening
                         join i in context.Istezanje on x.TreningId equals i.TreningId
                         where x.TreningId == tr.TreningId
                         select i;

                    foreach (Zagrijavanje a in query4.ToList())
                    {
                    context.Zagrijavanje.Remove(a);
                    }

                    foreach (Vjezba a in query2.ToList())
                    {
                        context.Vjezba.Remove(a);
                    }

                    foreach (Istezanje a in query3.ToList())
                    {
                        context.Istezanje.Remove(a);
                    }
                            context.Trening.Remove(tr);
                            context.SaveChanges();
                    }
	        }

            List<Trening> listaTreninga = new List<Trening>();
            if (query.ToList().Count == 0)
            {
                listaTreninga = new List<Trening>{
                    new Trening{ClanId = c.ClanId, ImeTreninga = "Trenutno nema treninga", TipTreninga = "",
                        BrojKrugova = 0, TreningId = 0}};
            }
            else
            {
                listaTreninga = query.ToList();
            }

            return View(listaTreninga);
        }

        public ActionResult IzbrisiTrening(int id = 0)
        {
            
            var query = from x in context.Trening
                        join y in context.Clan on x.ClanId equals y.ClanId
                        where x.TreningId == id
                        select new { x, y };

            var query1 = from x in context.Trening
                         join z in context.Zagrijavanje on x.TreningId equals z.TreningId
                         where x.TreningId == id
                         select z;

            var query2 = from x in context.Trening
                         join v in context.Vjezba on x.TreningId equals v.TreningId
                         where x.TreningId == id
                         select v;

            var query3 = from x in context.Trening
                         join i in context.Istezanje on x.TreningId equals i.TreningId
                         where x.TreningId == id
                         select i;

            var k = (int)query.FirstOrDefault().y.ClanId;

            foreach (Zagrijavanje a in query1.ToList())
            {
                context.Zagrijavanje.Remove(a);
            }

            foreach (Vjezba a in query2.ToList())
            {
                context.Vjezba.Remove(a);
            }

            foreach (Istezanje a in query3.ToList())
            {
                context.Istezanje.Remove(a);
            }

            Trening tr = (Trening)query.FirstOrDefault().x;
            context.Trening.Remove(tr);

            context.SaveChanges();

            return RedirectToAction("TreningPopis", new { id = k });
        }

        [HttpGet]
        public ActionResult IzmijeniTrening(int id = 0, int IzmijeniTrening = 0)
        {
            var query = from x in context.Trening
                        join y in context.Clan on x.ClanId equals y.ClanId
                        where x.TreningId == id
                        select new {x,y};

            var query1 = from x in context.Trening
                         join z in context.Zagrijavanje on x.TreningId equals z.TreningId
                        where x.TreningId == id
                        select z;

            var query2 = from x in context.Trening
                         join v in context.Vjezba on x.TreningId equals v.TreningId
                         where x.TreningId == id
                         select v;

            var query3 = from x in context.Trening
                         join i in context.Istezanje on x.TreningId equals i.TreningId
                         where x.TreningId == id
                         select i;


            TreningDataModel trm = new TreningDataModel()
            {
                ClanId = query.FirstOrDefault().y.ClanId,
                ClanIme = query.FirstOrDefault().y.Ime,
                ClanPrezime = query.FirstOrDefault().y.Prezime,
                TreningDatum = (DateTime)query.FirstOrDefault().x.DatumTreninga,
                TreningBrojKrugova = query.FirstOrDefault().x.BrojKrugova,
                TreningImeTreninga = query.FirstOrDefault().x.ImeTreninga,
                Napomena = query.FirstOrDefault().x.Napomena,
                TreningId = query.FirstOrDefault().x.TreningId
            };

            if (query1.FirstOrDefault() != null)
            {
                trm.ListaZagrijavanja = query1.ToList();
            }
            if (query2.FirstOrDefault() != null)
            {
                trm.ListaVjezbi = query2.ToList();
            }
            if (query3.FirstOrDefault() != null)
            {
                trm.ListaIstezanja = query3.ToList();
            }

            return View(trm);
        }

        [HttpPost]
        public ActionResult IzmijeniTrening(TreningDataModel trm)
        {
            var query = from x in context.Trening
                        where x.TreningId == trm.TreningId
                        select x;

            Trening tr = (Trening)query.Single();

            tr.ImeTreninga = trm.TreningImeTreninga;
            tr.BrojKrugova = trm.TreningBrojKrugova;
            tr.DatumTreninga = trm.TreningDatum;
            tr.Napomena = trm.Napomena;
            if (trm.TreningTip != null)
            {
                tr.TipTreninga = trm.TreningTip;
            };

            context.Entry(tr).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("TreningPopis", new { id = tr.ClanId });
        }

        [HttpGet]
        public ActionResult DetaljiTrening(int id = 0)
        {
            var query = from x in context.Trening
                        join y in context.Clan on x.ClanId equals y.ClanId
                        where x.TreningId == id
                        select new { x, y };

            var query1 = from x in context.Trening
                         join z in context.Zagrijavanje on x.TreningId equals z.TreningId
                         where x.TreningId == id
                         select z;

            var query2 = from x in context.Trening
                         join v in context.Vjezba on x.TreningId equals v.TreningId
                         where x.TreningId == id
                         select v;

            var query3 = from x in context.Trening
                         join i in context.Istezanje on x.TreningId equals i.TreningId
                         where x.TreningId == id
                         select i;


            TreningDataModel trm = new TreningDataModel()
            {
                ClanId = query.FirstOrDefault().y.ClanId,
                ClanIme = query.FirstOrDefault().y.Ime,
                ClanPrezime = query.FirstOrDefault().y.Prezime,
                TreningDatum = (DateTime)query.FirstOrDefault().x.DatumTreninga,
                TreningBrojKrugova = query.FirstOrDefault().x.BrojKrugova,
                TreningImeTreninga = query.FirstOrDefault().x.ImeTreninga,
                TreningId = query.FirstOrDefault().x.TreningId,
                Napomena = query.FirstOrDefault().x.Napomena
            };

            if (query1.FirstOrDefault() != null)
            {
                trm.ListaZagrijavanja = query1.ToList();
            }
            if (query2.FirstOrDefault() != null)
            {
                trm.ListaVjezbi = query2.ToList();
            }
            if (query3.FirstOrDefault() != null)
            {
                trm.ListaIstezanja = query3.ToList();
            }

            return View(trm);
        }

        [HttpGet]
        public ActionResult DodajTrening(int id = 0, int DodajVjezbu = 0)
        {
            Trening tr = new Trening();
            TreningDataModel trm = new TreningDataModel();

            if (DodajVjezbu == 0)
            {
                tr.ClanId = id;
                context.Trening.Add(tr);
                context.SaveChanges();

                trm.ClanId = id;

                var query = from x in context.Trening
                            join y in context.Clan on x.ClanId equals y.ClanId
                            where y.ClanId == id
                            orderby x.TreningId descending
                            select x;

                tr = (Trening)query.FirstOrDefault();
                trm.TreningId = tr.TreningId;

                Clan c = context.Clan.Find(id);
                trm.ClanIme = c.Ime;
                trm.ClanPrezime = c.Prezime;
            }
            else if(DodajVjezbu == 1)
            {
                trm.TreningId = id;

                var query4 = from x in context.Zagrijavanje
                             join y in context.Trening on x.TreningId equals y.TreningId
                             where y.TreningId == id
                             select x;

                trm.ListaZagrijavanja = (List<Zagrijavanje>)query4.ToList();

                var query3 = from x in context.Istezanje
                             join y in context.Trening on x.TreningId equals y.TreningId
                             where y.TreningId == id
                             select x;

                trm.ListaIstezanja = (List<Istezanje>)query3.ToList();

                var query2 = from x in context.Vjezba
                             join y in context.Trening on x.TreningId equals y.TreningId
                             where y.TreningId == id
                             select x;

                trm.ListaVjezbi = (List<Vjezba>)query2.ToList();

                var query1 = from x in context.Clan
                            join y in context.Trening on x.ClanId equals y.ClanId
                            where y.TreningId == id
                            select x;

                Clan c = (Clan)query1.Single();
                trm.ClanIme = c.Ime;
                trm.ClanPrezime = c.Prezime;
            }
            else
            {
                trm.TreningId = id;

                var query4 = from x in context.Zagrijavanje
                             join y in context.Trening on x.TreningId equals y.TreningId
                             where y.TreningId == id
                             select x;

                trm.ListaZagrijavanja = (List<Zagrijavanje>)query4.ToList();

                var query3 = from x in context.Istezanje
                             join y in context.Trening on x.TreningId equals y.TreningId
                             where y.TreningId == id
                             select x;

                trm.ListaIstezanja = (List<Istezanje>)query3.ToList();

                var query2 = from x in context.Vjezba
                            join y in context.Trening on x.TreningId equals y.TreningId
                            where y.TreningId == id
                            select x;

                trm.ListaVjezbi = (List<Vjezba>)query2.ToList();

                var query1 = from x in context.Clan
                            join y in context.Trening on x.ClanId equals y.ClanId
                            where y.TreningId == id
                            select x;

                Clan c = (Clan)query1.Single();
                trm.ClanIme = c.Ime;
                trm.ClanPrezime = c.Prezime;
            }

            return View(trm);
        }

        [HttpPost]
        public ActionResult DodajTrening(TreningDataModel trm)
        {
            var query = from x in context.Trening
                        where x.TreningId == trm.TreningId
                        select x;

            Trening tr = (Trening)query.Single();
            
            tr.ImeTreninga = trm.TreningImeTreninga;
            tr.BrojKrugova = trm.TreningBrojKrugova;
            tr.DatumTreninga = trm.TreningDatum;
            tr.Napomena = trm.Napomena;
            

            if(trm.TreningTip != null)
            {
            tr.TipTreninga = trm.TreningTip;
            };

            context.Entry(tr).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("TreningPopis", new {id = tr.ClanId });
        }

        /*Vjezbe*/
        [HttpGet]
        public ActionResult DodajVjezbuTrening(int id, int izmijeni = 0)
        {
            VjezbePopisLista vj = new VjezbePopisLista() { vjezbePopis = context.VjezbePopis.ToList(), TreningId = id, Izmijeni = izmijeni};
            ViewData["izmijeni"] = izmijeni;
            return View(vj);
        }

        [HttpPost]
        public ActionResult DodajVjezbuTrening(int vjpID = 0, int id = 0, int vjpIzmijeni = 0)
        {
            var query = from x in context.VjezbePopis
                         where x.VjezbeId == vjpID
                         select x;

            VjezbePopis vjp = (VjezbePopis)query.Single();


            Vjezba vj = new Vjezba() {ImeVjezbe = vjp.ImeVjezbe, TreningId = id };
            if (vjp.Info != null)
            {
                vj.Info = vjp.Info;
            }
            if (vjp.Slika !=null)
            {
                vj.Slika = vjp.Slika;
            }
 
            context.Vjezba.Add(vj);
            context.SaveChanges();

            ViewData["izmijeni"] = vjpIzmijeni;
            if (vjpIzmijeni != 0)
            {
                return RedirectToAction("IzmijeniTrening", new { id });
            }
            else
            {
                return RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
            }
        }

        [HttpGet]
        public ActionResult IzbrisiVjezbuTrening(int id = 0, int izmijeni = 0)
        {
            Vjezba vj = context.Vjezba.Find(id);
            id = vj.TreningId;
            context.Vjezba.Remove(vj);
            context.SaveChanges();

            if (izmijeni != 0)
            {
                return RedirectToAction("IzmijeniTrening", new { id });
            }
            else
            {
                return RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
            }
        }

        [HttpGet]
        public ActionResult DetaljiVjezbeTrening(int id = 0, int izmijeni = 0)
        {
            var query = from x in context.Vjezba
                        where x.VjezbaId == id
                        select x;
            Vjezba vj = (Vjezba)query.Single();
            ViewData["izmijeni"] = izmijeni;
            return View(vj);
        }

        /*Istezanje*/
        [HttpGet]
        public ActionResult DodajIstezanjeTrening(int id, int izmijeni = 0)
        {
            IstezanjeZagrijavanjeLista istp = new IstezanjeZagrijavanjeLista() { IstezanjePopis = context.IstezanjePopis.ToList(), TreningId = id, Izmijeni = izmijeni };
            ViewData["izmijeni"] = izmijeni;
            return View(istp);
        }

        [HttpPost]
        public ActionResult DodajIstezanjeTrening(int istpId = 0, int id = 0, int istpIzmijeni = 0)
        {
            var query = from x in context.IstezanjePopis
                        where x.IstezanjeId == istpId
                        select x;

            IstezanjePopis istp = (IstezanjePopis)query.Single();


            Istezanje ist = new Istezanje() { Naziv = istp.Naziv, TreningId = id };
            if (istp.Info != null)
            {
                ist.Info = istp.Info;
            }

            context.Istezanje.Add(ist);
            context.SaveChanges();

            ViewData["izmijeni"] = istpIzmijeni;
            if (istpIzmijeni != 0)
            {
                return RedirectToAction("IzmijeniTrening", new { id });
            }
            else
            {
                return RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
            }
        }

        [HttpGet]
        public ActionResult IzbrisiIstezanjeTrening(int id = 0, int izmijeni = 0)
        {
            Istezanje ist = context.Istezanje.Find(id);
            id = ist.TreningId;
            context.Istezanje.Remove(ist);
            context.SaveChanges();

            if (izmijeni != 0)
            {
                return RedirectToAction("IzmijeniTrening", new { id });
            }
            else
            {
                return RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
            }
        }

        [HttpGet]
        public ActionResult DetaljiIstezanjeTrening(int id = 0, int izmijeni = 0)
        {
            var query = from x in context.Istezanje
                        where x.IstezanjeId == id
                        select x;
            Istezanje ist = (Istezanje)query.Single();
            ViewData["izmijeni"] = izmijeni;
            return View(ist);
        }

        /*Zagrijavanje*/
        [HttpGet]
        public ActionResult DodajZagrijavanjeTrening(int id, int izmijeni = 0)
        {
            IstezanjeZagrijavanjeLista zgp = new IstezanjeZagrijavanjeLista() { ZagrijavanjePopis = context.ZagrijavanjePopis.ToList(), TreningId = id, Izmijeni = izmijeni };
            ViewData["izmijeni"] = izmijeni;
            return View(zgp);
        }

        [HttpPost]
        public ActionResult DodajZagrijavanjeTrening(int zgpId = 0, int id = 0, int zgpIzmijeni = 0)
        {
            var query = from x in context.ZagrijavanjePopis
                        where x.ZagrijavanjeId == zgpId
                        select x;

            ZagrijavanjePopis zgp = (ZagrijavanjePopis)query.Single();


            Zagrijavanje zg = new Zagrijavanje() { Naziv = zgp.Naziv, TreningId = id };
            if (zgp.Info != null)
            {
                zg.Info = zgp.Info;
            }

            context.Zagrijavanje.Add(zg);
            context.SaveChanges();
            ViewData["izmijeni"] = zgpIzmijeni;
            if (zgpIzmijeni != 0)
            {
                return RedirectToAction("IzmijeniTrening", new { id });
            }
            else
            {
                return RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
            }
        }

        [HttpGet]
        public ActionResult IzbrisiZagrijavanjeTrening(int id = 0, int izmijeni = 0)
        {
            Zagrijavanje zg = context.Zagrijavanje.Find(id);
            id = zg.TreningId;
            context.Zagrijavanje.Remove(zg);
            context.SaveChanges();

            if (izmijeni != 0)
            {
                return RedirectToAction("IzmijeniTrening", new { id });
            }
            else
            {
                return RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
            }
            
        }

        [HttpGet]
        public ActionResult DetaljiZagrijavanjeTrening(int id = 0, int izmijeni = 0)
        {
            var query = from x in context.Zagrijavanje
                        where x.ZagrijavanjeId == id
                        select x;
            Zagrijavanje zg = (Zagrijavanje)query.Single();
            ViewData["izmijeni"] = izmijeni;

            return View(zg);
        }

        [HttpPost]
        public ActionResult SpremiVjezbuInfo(string brojPonavljanja, string BrojSerija = null, string Tezina = null, string Odmor = null, int id = 0, int VjezbaId = 0)
        {

            var query = from x in context.Vjezba
                        where x.VjezbaId == VjezbaId
                        select x;

            Vjezba vj = (Vjezba)query.Single();
                vj.BrojPonavljanja = brojPonavljanja;
                vj.BrojSerija = BrojSerija;
                vj.Kilogrami = Tezina;
                vj.Odmor = Odmor;

            context.Entry(vj).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }
    }

    //klasa za nested forme

    //public class HttpParamActionAttribute : ActionNameSelectorAttribute
    //{
    //    public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
    //    {
    //        if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
    //            return true;

    //        if (!actionName.Equals("Action", StringComparison.InvariantCultureIgnoreCase))
    //            return false;

    //        var request = controllerContext.RequestContext.HttpContext.Request;
    //        return request[methodInfo.Name] != null;
    //    }
    //}
}