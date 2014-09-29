using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingPlanner.Models;

namespace TrainingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly TreningModelContainer _context = new TreningModelContainer();

        public ActionResult Index()
        {
            var lista = _context.Clan.ToList();
            return View(lista);
        }

        /****************Akcije sa zagrijavanjem, vjezbama i istezanjem****************/

        [HttpGet]
        public ActionResult ZagrijavanjePopis()
        {
            var listaZagrijavanja = _context.ZagrijavanjePopis.ToList();
            if (listaZagrijavanja.Count == 0)
            {
                listaZagrijavanja = new List<ZagrijavanjePopis>
                {
                    new ZagrijavanjePopis {ZagrijavanjeId = 0, Info = "", Naziv = ""}
                };
                return View(listaZagrijavanja);
            }
            return View(listaZagrijavanja);
        }

        public ActionResult IzbrisiZagrijavanje(int id = 0)
        {
            var zp = _context.ZagrijavanjePopis.Find(id);

            if (zp.ZagrijavanjeSlike.Count > 0)
            {
                var query = from x in _context.ZagrijavanjePopis
                    join i in _context.ZagrijavanjeSlike on x.ZagrijavanjeId equals i.ZagrijavanjePopisZagrijavanjeId
                    where x.ZagrijavanjeId == id
                    select i;

                foreach (var a in query.ToList())
                {
                    _context.ZagrijavanjeSlike.Remove(a);
                }
            }
            _context.ZagrijavanjePopis.Remove(zp);
            _context.SaveChanges();
            return RedirectToAction("ZagrijavanjePopis", "Home");
        }

        public ActionResult DetaljiZagrijavanja(int id = 0)
        {
            var zp = _context.ZagrijavanjePopis.Find(id);
            return View(zp);
        }

        [HttpGet]
        public ActionResult DodajNovoZagrijavanje()
        {
            var zp = new ZagrijavanjePopis();
            return View(zp);
        }

        [HttpPost]
        public ActionResult DodajNovoZagrijavanje(ZagrijavanjePopis zp, HttpPostedFileBase[] slike)
        {
            if (slike != null && slike.FirstOrDefault() != null)
            {
                var path = Server.MapPath("~/Content/ZagrijavanjeSlike/");
                foreach (var file in slike)
                {
                    var slika = new ZagrijavanjeSlike();
                    file.SaveAs(path + file.FileName);

                    slika.ZagrijavanjeSlikaIme = file.FileName;
                    slika.ZagrijavanjePopisZagrijavanjeId = zp.ZagrijavanjeId;

                    zp.ZagrijavanjeSlike.Add(slika);
                    _context.ZagrijavanjeSlike.Add(slika);
                }
            }

            _context.ZagrijavanjePopis.Add(zp);
            _context.SaveChanges();
            return RedirectToAction("ZagrijavanjePopis", "Home");
        }

        [HttpGet]
        public ActionResult IzmijeniZagrijavanje(int id = 0)
        {
            var zp = _context.ZagrijavanjePopis.Find(id);
            return View(zp);
        }

        [HttpPost]
        public ActionResult IzmijeniZagrijavanje(ZagrijavanjePopis zp, HttpPostedFileBase[] slike)
        {
            if (slike != null && slike.FirstOrDefault() != null)
            {
                var path = Server.MapPath("~/Content/ZagrijavanjeSlike/");

                foreach (var file in slike)
                {
                    var slika = new ZagrijavanjeSlike();
                    file.SaveAs(path + file.FileName);

                    slika.ZagrijavanjeSlikaIme = file.FileName;
                    slika.ZagrijavanjePopisZagrijavanjeId = zp.ZagrijavanjeId;

                    zp.ZagrijavanjeSlike.Add(slika);
                    _context.ZagrijavanjeSlike.Add(slika);
                }
            }

            _context.Entry(zp).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ZagrijavanjePopis", "Home");
        }

        [HttpGet]
        public ActionResult VjezbePopis()
        {
            var listaVjezbi = _context.VjezbePopis.ToList();
            if (listaVjezbi.Count == 0)
            {
                listaVjezbi = new List<VjezbePopis>
                {
                    new VjezbePopis {VjezbeId = 0, ImeVjezbe = "", Info = "", Slika = null}
                };
                return View(listaVjezbi);
            }
            return View(listaVjezbi);
        }

        public ActionResult IzbrisiVjezbu(int id = 0)
        {
            var vjp = _context.VjezbePopis.Find(id);

            if (vjp.VjezbeSlike.Count > 0)
            {
                var query = from x in _context.VjezbePopis
                    join i in _context.VjezbeSlike on x.VjezbeId equals i.VjezbePopisVjezbeId
                    where x.VjezbeId == id
                    select i;

                foreach (var a in query.ToList())
                {
                    _context.VjezbeSlike.Remove(a);
                }
            }

            _context.VjezbePopis.Remove(vjp);
            _context.SaveChanges();
            return RedirectToAction("VjezbePopis", "Home");
        }

        public ActionResult DetaljiVjezbe(int id = 0)
        {
            var vjp = _context.VjezbePopis.Find(id);
            return View(vjp);
        }

        [HttpGet]
        public ActionResult DodajNovuVjezbu()
        {
            var vjp = new VjezbePopis();
            return View(vjp);
        }

        [HttpPost]
        public ActionResult DodajNovuVjezbu(VjezbePopis vjp, HttpPostedFileBase[] slike)
        {
            if (slike != null && slike.FirstOrDefault() != null)
            {
                var path = Server.MapPath("~/Content/VjezbeSlike/");
                foreach (var file in slike)
                {
                    var slika = new VjezbeSlike();
                    file.SaveAs(path + file.FileName);

                    slika.VjezbeSlikaIme = file.FileName;
                    slika.VjezbePopisVjezbeId = vjp.VjezbeId;

                    vjp.VjezbeSlike.Add(slika);
                    _context.VjezbeSlike.Add(slika);
                }
            }

            _context.VjezbePopis.Add(vjp);
            _context.SaveChanges();
            return RedirectToAction("VjezbePopis", "Home");
        }

        [HttpGet]
        public ActionResult IzmijeniVjezbu(int id = 0)
        {
            var vjp = _context.VjezbePopis.Find(id);
            return View(vjp);
        }

        [HttpPost]
        public ActionResult IzmijeniVjezbu(VjezbePopis vjp, HttpPostedFileBase[] slike)
        {
            if (slike != null && slike.FirstOrDefault() != null)
            {
                var path = Server.MapPath("~/Content/VjezbeSlike/");

                foreach (var file in slike)
                {
                    var slika = new VjezbeSlike();
                    file.SaveAs(path + file.FileName);

                    slika.VjezbeSlikaIme = file.FileName;
                    slika.VjezbePopisVjezbeId = vjp.VjezbeId;

                    vjp.VjezbeSlike.Add(slika);
                    _context.VjezbeSlike.Add(slika);
                }
            }

            _context.Entry(vjp).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("VjezbePopis", "Home");
        }

        [HttpGet]
        public ActionResult IstezanjePopis()
        {
            var listaIstezanja = _context.IstezanjePopis.ToList();
            if (listaIstezanja.Count == 0)
            {
                listaIstezanja = new List<IstezanjePopis> {new IstezanjePopis {IstezanjeId = 0, Naziv = "", Info = ""}};
                return View(listaIstezanja);
            }
            return View(listaIstezanja);
        }

        public ActionResult IzbrisiIstezanje(int id = 0)
        {
            var ip = _context.IstezanjePopis.Find(id);

            if (ip.IstezanjeSlike.Count > 0)
            {
                var query = from x in _context.IstezanjePopis
                    join i in _context.IstezanjeSlike on x.IstezanjeId equals i.IstezanjePopisIstezanjeId
                    where x.IstezanjeId == id
                    select i;

                foreach (var a in query.ToList())
                {
                    _context.IstezanjeSlike.Remove(a);
                }
            }

            _context.IstezanjePopis.Remove(ip);
            _context.SaveChanges();
            return RedirectToAction("IstezanjePopis", "Home");
        }

        public ActionResult DetaljiIstezanja(int id = 0)
        {
            var ip = _context.IstezanjePopis.Find(id);
            return View(ip);
        }

        [HttpGet]
        public ActionResult DodajNovoIstezanje()
        {
            var ip = new IstezanjePopis();
            return View(ip);
        }

        [HttpPost]
        public ActionResult DodajNovoIstezanje(IstezanjePopis ip, HttpPostedFileBase[] slike)
        {
            if (slike != null && slike.FirstOrDefault() != null)
            {
                var path = Server.MapPath("~/Content/IstezanjeSlike/");
                foreach (var file in slike)
                {
                    var slika = new IstezanjeSlike();
                    file.SaveAs(path + file.FileName);

                    slika.IstezanjeSlikaIme = file.FileName;
                    slika.IstezanjePopisIstezanjeId = ip.IstezanjeId;

                    ip.IstezanjeSlike.Add(slika);
                    _context.IstezanjeSlike.Add(slika);
                }
            }

            _context.IstezanjePopis.Add(ip);
            _context.SaveChanges();
            return RedirectToAction("IstezanjePopis", "Home");
        }

        [HttpGet]
        public ActionResult IzmijeniIstezanje(int id = 0)
        {
            var ip = _context.IstezanjePopis.Find(id);
            return View(ip);
        }

        [HttpPost]
        public ActionResult IzmijeniIstezanje(IstezanjePopis ip, HttpPostedFileBase[] slike)
        {
            if (slike != null && slike.FirstOrDefault() != null)
            {
                var path = Server.MapPath("~/Content/IstezanjeSlike/");

                foreach (var file in slike)
                {
                    var slika = new IstezanjeSlike();
                    file.SaveAs(path + file.FileName);

                    slika.IstezanjeSlikaIme = file.FileName;
                    slika.IstezanjePopisIstezanjeId = ip.IstezanjeId;

                    ip.IstezanjeSlike.Add(slika);
                    _context.IstezanjeSlike.Add(slika);
                }
            }

            _context.Entry(ip).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("IstezanjePopis", "Home");
        }

        /****************Akcije sa clanovima i testovima****************/

        [HttpGet]
        public ActionResult DodajClana()
        {
            var c = new Clan();
            return View(c);
        }

        [HttpPost]
        public ActionResult DodajClana(Clan c)
        {
            var age = (short) ((DateTime.Now - c.GodinaRodenja).TotalDays/365.242199);
            c.GodineStarosti = age;
            _context.Clan.Add(c);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult IzbrisiClana(int id)
        {
            var c = _context.Clan.Find(id);

            var query = from x in _context.Trening
                where x.ClanId == id
                select x;

            if (query.FirstOrDefault() != null)
            {
                foreach (var a in query.ToList())
                {
                    var query1 = from x in _context.Trening
                        join z in _context.Zagrijavanje on x.TreningId equals z.TreningId
                        where x.TreningId == a.TreningId
                        select z;

                    var query2 = from x in _context.Trening
                        join v in _context.Vjezba on x.TreningId equals v.TreningId
                        where x.TreningId == a.TreningId
                        select v;

                    var query3 = from x in _context.Trening
                        join i in _context.Istezanje on x.TreningId equals i.TreningId
                        where x.TreningId == a.TreningId
                        select i;

                    foreach (var b in query1.ToList())
                    {
                        _context.Zagrijavanje.Remove(b);
                    }

                    foreach (var b in query2.ToList())
                    {
                        _context.Vjezba.Remove(b);
                    }

                    foreach (var b in query3.ToList())
                    {
                        _context.Istezanje.Remove(b);
                    }

                    _context.Trening.Remove(a);
                }
            }

            var queryTest = from x in _context.Test
                where x.ClanId == id
                select x;

            if (queryTest.FirstOrDefault() != null)
            {
                foreach (var a in queryTest.ToList())
                {
                    _context.Test.Remove(a);
                }
            }

            _context.Clan.Remove(c);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult IzmijeniClana(int id = 0)
        {
            var c = _context.Clan.Find(id);
            return View(c);
        }

        [HttpPost]
        public ActionResult IzmijeniClana(Clan c)
        {
            var age = (short) ((DateTime.Now - c.GodinaRodenja).TotalDays/365.242199);
            c.GodineStarosti = age;
            _context.Entry(c).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DetaljiClana(int id = 0)
        {
            var c = _context.Clan.Find(id);

            var queryTest = from x in _context.Test
                where x.ClanId == id
                select x;

            var ctm = new ClanTestModel {Clan = c, ListaTest = queryTest.ToList()};

            return View(ctm);
        }

        [HttpGet]
        public ActionResult Test(int id = 0)
        {
            var query = from x in _context.Test
                where x.ClanId == id
                select x;

            var query1 = from x in _context.Clan
                where x.ClanId == id
                select x;

            var c = query1.Single();

            var listaT = new TestList {Clan = c};

            if (query.FirstOrDefault() != null)
            {
                listaT.ListaTestova = query.ToList();
            }

            return View(listaT);
        }

        [HttpGet]
        public ActionResult DodajTest(int id = 0)
        {
            var t = new Test {ClanId = id};
            return View(t);
        }

        [HttpPost]
        public ActionResult DodajTest(Test t, HttpPostedFileBase[] slike)
        {
            if (slike != null && slike.FirstOrDefault() != null)
            {
                var path = Server.MapPath("~/Content/Slike/");
                foreach (var file in slike)
                {
                    var slika = new Slika();
                    file.SaveAs(path + file.FileName);

                    slika.SlikaIme = file.FileName;
                    slika.TestTestId = t.TestId;

                    t.Slika.Add(slika);
                    _context.Slika.Add(slika);
                }
            }
            _context.Test.Add(t);
            _context.SaveChanges();

            return RedirectToAction("Test", new {id = t.ClanId});
        }

        public ActionResult IzbrisiTest(int id)
        {
            var t = _context.Test.Find(id);

            if (t.Slika.Count > 0)
            {
                var query = from x in _context.Test
                    join i in _context.Slika on x.TestId equals i.TestTestId
                    where x.TestId == id
                    select i;

                foreach (var a in query.ToList())
                {
                    _context.Slika.Remove(a);
                }
            }
            _context.Test.Remove(t);
            _context.SaveChanges();

            return RedirectToAction("Test", new {id = t.ClanId});
        }

        [HttpGet]
        public ActionResult IzmijeniTest(int id = 0)
        {
            var t = _context.Test.Find(id);
            return View(t);
        }

        [HttpPost]
        public ActionResult IzmijeniTest(Test t, HttpPostedFileBase[] slike)
        {
            if (slike != null && slike.FirstOrDefault() != null)
            {
                var path = Server.MapPath("~/Content/Slike/");
                foreach (var file in slike)
                {
                    var slika = new Slika();
                    file.SaveAs(path + file.FileName);

                    slika.SlikaIme = file.FileName;
                    slika.TestTestId = t.TestId;

                    t.Slika.Add(slika);
                    _context.Slika.Add(slika);
                }
            }

            _context.Entry(t).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Test", new {id = t.ClanId});
        }

        public ActionResult DetaljiTest(int id)
        {
            var t = _context.Test.Find(id);
            return View(t);

        }

        /*****************Akcije sa treningom*****************/

        [HttpGet]
        public ActionResult TreningPopis(int id = 0)
        {
            var query1 = from x in _context.Clan
                where x.ClanId == id
                select x;

            var c = query1.Single();

            ViewData["ImeClana"] = c.Ime;
            ViewData["PrezimeClana"] = c.Prezime;

            var query = from x in _context.Trening
                join y in _context.Clan on x.ClanId equals y.ClanId
                where y.ClanId == id
                orderby x.DatumTreninga ascending
                select x;

            foreach (var tr in query.ToList())
            {
                if (tr.ImeTreninga == null)
                {
                    var query4 = from x in _context.Trening
                        join z in _context.Zagrijavanje on x.TreningId equals z.TreningId
                        where x.TreningId == tr.TreningId
                        select z;

                    var query2 = from x in _context.Trening
                        join v in _context.Vjezba on x.TreningId equals v.TreningId
                        where x.TreningId == tr.TreningId
                        select v;

                    var query3 = from x in _context.Trening
                        join i in _context.Istezanje on x.TreningId equals i.TreningId
                        where x.TreningId == tr.TreningId
                        select i;

                    foreach (var a in query4.ToList())
                    {
                        _context.Zagrijavanje.Remove(a);
                    }

                    foreach (var a in query2.ToList())
                    {
                        _context.Vjezba.Remove(a);
                    }

                    foreach (var a in query3.ToList())
                    {
                        _context.Istezanje.Remove(a);
                    }
                    _context.Trening.Remove(tr);
                    _context.SaveChanges();
                }
            }

            List<Trening> listaTreninga;
            if (query.ToList().Count == 0)
            {
                listaTreninga = new List<Trening>
                {
                    new Trening
                    {
                        ClanId = c.ClanId,
                        ImeTreninga = "Trenutno nema treninga",
                        TipTreninga = "",
                        BrojKrugova = 0,
                        TreningId = 0
                    }
                };
            }
            else
            {
                listaTreninga = query.ToList();
            }

            return View(listaTreninga);
        }

        public ActionResult IzbrisiTrening(int id = 0)
        {
            var query = from x in _context.Trening
                join y in _context.Clan on x.ClanId equals y.ClanId
                where x.TreningId == id
                select new {x, y};

            var query1 = from x in _context.Trening
                join z in _context.Zagrijavanje on x.TreningId equals z.TreningId
                where x.TreningId == id
                select z;

            var query2 = from x in _context.Trening
                join v in _context.Vjezba on x.TreningId equals v.TreningId
                where x.TreningId == id
                select v;

            var query3 = from x in _context.Trening
                join i in _context.Istezanje on x.TreningId equals i.TreningId
                where x.TreningId == id
                select i;

            var firstOrDefault = query.FirstOrDefault();
            if (firstOrDefault != null)
            {
                var k = firstOrDefault.y.ClanId;

                foreach (var a in query1.ToList())
                {
                    _context.Zagrijavanje.Remove(a);
                }

                foreach (var a in query2.ToList())
                {
                    _context.Vjezba.Remove(a);
                }

                foreach (var a in query3.ToList())
                {
                    _context.Istezanje.Remove(a);
                }

                var tr = firstOrDefault.x;
                _context.Trening.Remove(tr);
                _context.SaveChanges();

                return RedirectToAction("TreningPopis", new {id = k});
            }
            return null;
        }

        [HttpGet]
        public ActionResult IzmijeniTrening(int id = 0, int izmijeniTrening = 0)
        {
            var query = from x in _context.Trening
                join y in _context.Clan on x.ClanId equals y.ClanId
                where x.TreningId == id
                select new {x, y};

            var query1 = from x in _context.Trening
                join z in _context.Zagrijavanje on x.TreningId equals z.TreningId
                where x.TreningId == id
                select z;

            var query2 = from x in _context.Trening
                join v in _context.Vjezba on x.TreningId equals v.TreningId
                where x.TreningId == id
                select v;

            var query3 = from x in _context.Trening
                join i in _context.Istezanje on x.TreningId equals i.TreningId
                where x.TreningId == id
                select i;

            var firstOrDefault = query.FirstOrDefault();
            if (firstOrDefault != null)
            {
                if (firstOrDefault.x.DatumTreninga != null)
                {
                    var trm = new TreningDataModel
                    {
                        ClanId = firstOrDefault.y.ClanId,
                        ClanIme = firstOrDefault.y.Ime,
                        ClanPrezime = firstOrDefault.y.Prezime,
                        TreningDatum = (DateTime) firstOrDefault.x.DatumTreninga,
                        TreningBrojKrugova = firstOrDefault.x.BrojKrugova,
                        TreningImeTreninga = firstOrDefault.x.ImeTreninga,
                        Napomena = firstOrDefault.x.Napomena,
                        TreningId = firstOrDefault.x.TreningId
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
            }
            return null;
        }

        [HttpPost]
        public ActionResult IzmijeniTrening(TreningDataModel trm)
        {
            var query = from x in _context.Trening
                where x.TreningId == trm.TreningId
                select x;

            var tr = query.Single();

            tr.ImeTreninga = trm.TreningImeTreninga;
            tr.BrojKrugova = trm.TreningBrojKrugova;
            tr.DatumTreninga = trm.TreningDatum;
            tr.Napomena = trm.Napomena;
            if (trm.TreningTip != null)
            {
                tr.TipTreninga = trm.TreningTip;
            }

            _context.Entry(tr).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("TreningPopis", new {id = tr.ClanId});
        }

        [HttpGet]
        public ActionResult DetaljiTrening(int id = 0)
        {
            var query = from x in _context.Trening
                join y in _context.Clan on x.ClanId equals y.ClanId
                where x.TreningId == id
                select new {x, y};

            var query1 = from x in _context.Trening
                join z in _context.Zagrijavanje on x.TreningId equals z.TreningId
                where x.TreningId == id
                select z;

            var query2 = from x in _context.Trening
                join v in _context.Vjezba on x.TreningId equals v.TreningId
                where x.TreningId == id
                select v;

            var query3 = from x in _context.Trening
                join i in _context.Istezanje on x.TreningId equals i.TreningId
                where x.TreningId == id
                select i;

            var firstOrDefault = query.FirstOrDefault();
            if (firstOrDefault != null)
            {
                if (firstOrDefault.x.DatumTreninga != null)
                {
                    var trm = new TreningDataModel
                    {
                        ClanId = firstOrDefault.y.ClanId,
                        ClanIme = firstOrDefault.y.Ime,
                        ClanPrezime = firstOrDefault.y.Prezime,
                        TreningDatum = (DateTime) firstOrDefault.x.DatumTreninga,
                        TreningBrojKrugova = firstOrDefault.x.BrojKrugova,
                        TreningImeTreninga = firstOrDefault.x.ImeTreninga,
                        TreningId = firstOrDefault.x.TreningId,
                        Napomena = firstOrDefault.x.Napomena
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
            }
            return null;
        }

        [HttpGet]
        public ActionResult DodajTrening(int id = 0, int dodajVjezbu = 0)
        {
            var tr = new Trening();
            var trm = new TreningDataModel();

            if (dodajVjezbu == 0)
            {
                tr.ClanId = id;
                _context.Trening.Add(tr);
                _context.SaveChanges();

                trm.ClanId = id;

                var query = from x in _context.Trening
                    join y in _context.Clan on x.ClanId equals y.ClanId
                    where y.ClanId == id
                    orderby x.TreningId descending
                    select x;

                tr = query.FirstOrDefault();
                if (tr != null) trm.TreningId = tr.TreningId;

                var c = _context.Clan.Find(id);
                trm.ClanIme = c.Ime;
                trm.ClanPrezime = c.Prezime;
            }
            else if (dodajVjezbu == 1)
            {
                trm.TreningId = id;

                var query4 = from x in _context.Zagrijavanje
                    join y in _context.Trening on x.TreningId equals y.TreningId
                    where y.TreningId == id
                    select x;

                trm.ListaZagrijavanja = query4.ToList();

                var query3 = from x in _context.Istezanje
                    join y in _context.Trening on x.TreningId equals y.TreningId
                    where y.TreningId == id
                    select x;

                trm.ListaIstezanja = query3.ToList();

                var query2 = from x in _context.Vjezba
                    join y in _context.Trening on x.TreningId equals y.TreningId
                    where y.TreningId == id
                    select x;

                trm.ListaVjezbi = query2.ToList();

                var query1 = from x in _context.Clan
                    join y in _context.Trening on x.ClanId equals y.ClanId
                    where y.TreningId == id
                    select x;

                var c = query1.Single();
                trm.ClanIme = c.Ime;
                trm.ClanPrezime = c.Prezime;
            }
            else
            {
                trm.TreningId = id;

                var query4 = from x in _context.Zagrijavanje
                    join y in _context.Trening on x.TreningId equals y.TreningId
                    where y.TreningId == id
                    select x;

                trm.ListaZagrijavanja = query4.ToList();

                var query3 = from x in _context.Istezanje
                    join y in _context.Trening on x.TreningId equals y.TreningId
                    where y.TreningId == id
                    select x;

                trm.ListaIstezanja = query3.ToList();

                var query2 = from x in _context.Vjezba
                    join y in _context.Trening on x.TreningId equals y.TreningId
                    where y.TreningId == id
                    select x;

                trm.ListaVjezbi = query2.ToList();

                var query1 = from x in _context.Clan
                    join y in _context.Trening on x.ClanId equals y.ClanId
                    where y.TreningId == id
                    select x;

                var c = query1.Single();
                trm.ClanIme = c.Ime;
                trm.ClanPrezime = c.Prezime;
            }
            return View(trm);
        }

        [HttpPost]
        public ActionResult DodajTrening(TreningDataModel trm)
        {
            var query = from x in _context.Trening
                where x.TreningId == trm.TreningId
                select x;

            var tr = query.Single();

            tr.ImeTreninga = trm.TreningImeTreninga;
            tr.BrojKrugova = trm.TreningBrojKrugova;
            tr.DatumTreninga = trm.TreningDatum;
            tr.Napomena = trm.Napomena;

            if (trm.TreningTip != null)
            {
                tr.TipTreninga = trm.TreningTip;
            }

            _context.Entry(tr).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("TreningPopis", new {id = tr.ClanId});
        }

        /*Vjezbe*/

        [HttpGet]
        public ActionResult DodajVjezbuTrening(int id, int izmijeni = 0)
        {
            var vj = new VjezbePopisLista
            {
                VjezbePopis = _context.VjezbePopis.ToList(),
                TreningId = id,
                Izmijeni = izmijeni
            };
            ViewData["izmijeni"] = izmijeni;
            return View(vj);
        }

        [HttpPost]
        public ActionResult DodajVjezbuTrening(int vjpId = 0, int id = 0, int vjpIzmijeni = 0)
        {
            var query = from x in _context.VjezbePopis
                where x.VjezbeId == vjpId
                select x;

            var vjp = query.Single();

            var vj = new Vjezba {ImeVjezbe = vjp.ImeVjezbe, TreningId = id};
            if (vjp.Info != null)
            {
                vj.Info = vjp.Info;
            }
            if (vjp.Slika != null)
            {
                vj.Slika = vjp.Slika;
            }

            _context.Vjezba.Add(vj);
            _context.SaveChanges();

            ViewData["izmijeni"] = vjpIzmijeni;
            return vjpIzmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new {id})
                : RedirectToAction("DodajTrening", new {id, DodajVjezbu = 2});
        }

        [HttpGet]
        public ActionResult IzbrisiVjezbuTrening(int id = 0, int izmijeni = 0)
        {
            var vj = _context.Vjezba.Find(id);
            id = vj.TreningId;
            _context.Vjezba.Remove(vj);
            _context.SaveChanges();

            return izmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new {id})
                : RedirectToAction("DodajTrening", new {id, DodajVjezbu = 2});
        }

        [HttpGet]
        public ActionResult DetaljiVjezbeTrening(int id = 0, int izmijeni = 0)
        {
            var query = from x in _context.Vjezba
                where x.VjezbaId == id
                select x;
            var vj = query.Single();
            ViewData["izmijeni"] = izmijeni;
            return View(vj);
        }

        /*Istezanje*/

        [HttpGet]
        public ActionResult DodajIstezanjeTrening(int id, int izmijeni = 0)
        {
            var istp = new IstezanjeZagrijavanjeLista
            {
                IstezanjePopis = _context.IstezanjePopis.ToList(),
                TreningId = id,
                Izmijeni = izmijeni
            };
            ViewData["izmijeni"] = izmijeni;
            return View(istp);
        }

        [HttpPost]
        public ActionResult DodajIstezanjeTrening(int istpId = 0, int id = 0, int istpIzmijeni = 0)
        {
            var query = from x in _context.IstezanjePopis
                where x.IstezanjeId == istpId
                select x;

            var istp = query.Single();

            var ist = new Istezanje {Naziv = istp.Naziv, TreningId = id};
            if (istp.Info != null)
            {
                ist.Info = istp.Info;
            }

            _context.Istezanje.Add(ist);
            _context.SaveChanges();

            ViewData["izmijeni"] = istpIzmijeni;
            return istpIzmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new {id})
                : RedirectToAction("DodajTrening", new {id, DodajVjezbu = 2});
        }

        [HttpGet]
        public ActionResult IzbrisiIstezanjeTrening(int id = 0, int izmijeni = 0)
        {
            var ist = _context.Istezanje.Find(id);
            id = ist.TreningId;
            _context.Istezanje.Remove(ist);
            _context.SaveChanges();

            return izmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new {id})
                : RedirectToAction("DodajTrening", new {id, DodajVjezbu = 2});
        }

        [HttpGet]
        public ActionResult DetaljiIstezanjeTrening(int id = 0, int izmijeni = 0)
        {
            var query = from x in _context.Istezanje
                where x.IstezanjeId == id
                select x;
            var ist = query.Single();
            ViewData["izmijeni"] = izmijeni;
            return View(ist);
        }

        /*Zagrijavanje*/

        [HttpGet]
        public ActionResult DodajZagrijavanjeTrening(int id, int izmijeni = 0)
        {
            var zgp = new IstezanjeZagrijavanjeLista
            {
                ZagrijavanjePopis = _context.ZagrijavanjePopis.ToList(),
                TreningId = id,
                Izmijeni = izmijeni
            };
            ViewData["izmijeni"] = izmijeni;
            return View(zgp);
        }

        [HttpPost]
        public ActionResult DodajZagrijavanjeTrening(int zgpId = 0, int id = 0, int zgpIzmijeni = 0)
        {
            var query = from x in _context.ZagrijavanjePopis
                where x.ZagrijavanjeId == zgpId
                select x;

            var zgp = query.Single();

            var zg = new Zagrijavanje {Naziv = zgp.Naziv, TreningId = id};
            if (zgp.Info != null)
            {
                zg.Info = zgp.Info;
            }

            _context.Zagrijavanje.Add(zg);
            _context.SaveChanges();
            ViewData["izmijeni"] = zgpIzmijeni;
            return zgpIzmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new {id})
                : RedirectToAction("DodajTrening", new {id, DodajVjezbu = 2});
        }

        [HttpGet]
        public ActionResult IzbrisiZagrijavanjeTrening(int id = 0, int izmijeni = 0)
        {
            var zg = _context.Zagrijavanje.Find(id);
            id = zg.TreningId;
            _context.Zagrijavanje.Remove(zg);
            _context.SaveChanges();

            return izmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new {id})
                : RedirectToAction("DodajTrening", new {id, DodajVjezbu = 2});
        }

        [HttpGet]
        public ActionResult DetaljiZagrijavanjeTrening(int id = 0, int izmijeni = 0)
        {
            var query = from x in _context.Zagrijavanje
                where x.ZagrijavanjeId == id
                select x;
            var zg = query.Single();
            ViewData["izmijeni"] = izmijeni;

            return View(zg);
        }

        [HttpPost]
        public ActionResult SpremiZagrijavanjeInfo(string puls, string tempo = null, string napomena = null, int id = 0,
            int ZagrijavanjeId = 0)
        {
            var query = from x in _context.Zagrijavanje
                where x.ZagrijavanjeId == ZagrijavanjeId
                select x;

            var zg = query.Single();
            zg.Tempo = tempo;
            zg.Puls = puls;
            zg.Napomena = napomena;

            _context.Entry(zg).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("DodajTrening", new {id, DodajVjezbu = 2});
        }

        [HttpPost]
        public ActionResult SpremiVjezbuInfo(string brojPonavljanja, string brojSerija = null, string tezina = null,
            string odmor = null, int id = 0, int vjezbaId = 0)
        {
            var query = from x in _context.Vjezba
                where x.VjezbaId == vjezbaId
                select x;

            var vj = query.Single();
            vj.BrojPonavljanja = brojPonavljanja;
            vj.BrojSerija = brojSerija;
            vj.Kilogrami = tezina;
            vj.Odmor = odmor;

            _context.Entry(vj).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("DodajTrening", new {id, DodajVjezbu = 2});
        }

        [HttpPost]
        public ActionResult SpremiIstezanjeInfo(string vrijemeIzdrzaja, string vrstaIstezanja = null, int id = 0,
            int istezanjeId = 0)
        {
            var query = from x in _context.Istezanje
                where x.IstezanjeId == istezanjeId
                select x;

            var ist = query.Single();
            ist.VrijemeIzdrzaja = vrijemeIzdrzaja;
            ist.VrstaIstezanja = vrstaIstezanja;

            _context.Entry(ist).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("DodajTrening", new {id, DodajVjezbu = 2});
        }

        public ActionResult IzbrisiSliku(int id, int? slika)
        {
            var t = _context.Test.Find(id);
            var imageToDelete = _context.Slika.Find(slika);

            _context.Slika.Remove(imageToDelete);
            _context.SaveChanges();

            t.Slika.Remove(imageToDelete);
            _context.SaveChanges();

            return View("IzmijeniTest", t);
        }

        public ActionResult IzbrisiSlikuZagrijavanje(int id, int? slika)
        {
            var t = _context.ZagrijavanjePopis.Find(id);
            var imageToDelete = _context.ZagrijavanjeSlike.Find(slika);

            _context.ZagrijavanjeSlike.Remove(imageToDelete);
            _context.SaveChanges();

            t.ZagrijavanjeSlike.Remove(imageToDelete);
            _context.SaveChanges();

            return View("IzmijeniZagrijavanje", t);
        }

        public ActionResult IzbrisiSlikuVjezbe(int id, int? slika)
        {
            var t = _context.VjezbePopis.Find(id);
            var imageToDelete = _context.VjezbeSlike.Find(slika);

            _context.VjezbeSlike.Remove(imageToDelete);
            _context.SaveChanges();

            t.VjezbeSlike.Remove(imageToDelete);
            _context.SaveChanges();

            return View("IzmijeniVjezbu", t);
        }

        public ActionResult IzbrisiSlikuIstezanje(int id, int? slika)
        {
            var t = _context.IstezanjePopis.Find(id);
            var imageToDelete = _context.IstezanjeSlike.Find(slika);

            _context.IstezanjeSlike.Remove(imageToDelete);
            _context.SaveChanges();

            t.IstezanjeSlike.Remove(imageToDelete);
            _context.SaveChanges();

            return View("IzmijeniIstezanje", t);
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