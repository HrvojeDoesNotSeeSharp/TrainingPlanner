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

            //provjeri dali je ovo zagrijavanje ukljuceno u neki trening i vrati id clana
            var zagrijavanja = from x in _context.Zagrijavanje
                               join i in _context.ZagrijavanjePopis on x.ZagrijavanjePopisZagrijavanjeId equals i.ZagrijavanjeId
                               where i.ZagrijavanjeId == id
                               select x.ZagrijavanjePopisZagrijavanjeId;

            var provjeriTrening = from t in _context.Trening
                                  join z in _context.Zagrijavanje on t.TreningId equals z.TreningId
                                  where z.ZagrijavanjePopisZagrijavanjeId == zagrijavanja.FirstOrDefault()
                                  group t by t.Clan into grp
                                  select grp.Key;

            List<String> clanovi = new List<string>();

            foreach (Clan c in provjeriTrening.ToList())
            {
                clanovi.Add(c.Ime + " " + c.Prezime + ", ");
            }
            //iduca linija je provjera koja ne brise zagrijavanje ako postoji trening sa tin zagrijavanjem
            if (clanovi.Count > 0)
            {
                string joined = String.Concat(clanovi.ToArray());
                return Content("<script language='javascript' type='text/javascript'>alert('Ovi clanovi koriste zagrijavanje:" + joined + "');"
                    + "window.location.href='../ZagrijavanjePopis';</script>");
            }
            else
            {
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
        }

        public ActionResult DetaljiZagrijavanja(int id = 0, int izmijeni = 0, int trening = 0, int treningId = 0, int counter = 0)
        {
            var zp = _context.ZagrijavanjePopis.Find(id);
            if (izmijeni == 1)
            {
                ViewData["izmijeni"] = izmijeni;
            }
            if (trening == 1)
            {
                ViewData["trening"] = trening;
            }
            if (treningId > 0)
            {
                ViewData["treningId"] = treningId;
            }
            ViewData["counter"] = counter;

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
            if (ModelState.IsValid)
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
            return RedirectToAction("DodajNovoZagrijavanje");
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
            if (ModelState.IsValid)
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
            return RedirectToAction("IzmijeniZagrijavanje", zp);
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

            //provjeri da li je ova vjezba ukljucena u neki trening i vrati id clana
            var vjezbe = from x in _context.Vjezba
                               join v in _context.VjezbePopis on x.VjezbePopisVjezbeId equals v.VjezbeId
                               where v.VjezbeId == id
                               select x.VjezbePopisVjezbeId;

            var sekcije = from s in _context.SekcijaVjezbi
                                  join v in _context.Vjezba on s.SekcijaId equals v.SekcijaId
                                  where v.VjezbePopisVjezbeId == vjezbe.FirstOrDefault()
                                  group s by s.SekcijaId into grp
                                  select grp.Key;

            List<Clan> provjeriTreningupdejtanalista = new List<Clan>();
            IQueryable<Clan> provjeriTrening;
            foreach (var s in sekcije.ToList())
	            {
		            provjeriTrening = from t in _context.Trening
                                  join se in _context.SekcijaVjezbi on t.TreningId equals se.TreningId
                                  where se.SekcijaId == s
                                  group t by t.Clan into grp
                                  select grp.Key;

                if(!provjeriTreningupdejtanalista.Contains(provjeriTrening.FirstOrDefault()))
                {
                    provjeriTreningupdejtanalista.Add(provjeriTrening.FirstOrDefault());
                }
	            }

            List<String> clanovi = new List<string>();

            foreach (Clan c in provjeriTreningupdejtanalista)
            {
                clanovi.Add(c.Ime + " " + c.Prezime + ", ");
            }
            //iduca linija je provjera koja ne brise vjezbe ako postoji trening sa tom vjezbom
            if (clanovi.Count > 0)
            {
                string joined = String.Concat(clanovi.ToArray());
                return Content("<script language='javascript' type='text/javascript'>alert('Ovi clanovi koriste vjezbu:" + joined + "');"
                    + "window.location.href='../VjezbePopis';</script>");
            }
            else
            {
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
        }

        public ActionResult DetaljiVjezbe(int id = 0, int izmijeni = 0, int trening = 0, int treningId = 0, int counter = 0)
        {
            var vjp = _context.VjezbePopis.Find(id);

            if (izmijeni == 1)
            {
                ViewData["izmijeni"] = izmijeni;
            }
            if (trening == 1)
            {
                ViewData["trening"] = trening;
            }
            if (treningId > 0)
            {
                ViewData["treningId"] = treningId;
            }

            ViewData["counter"] = counter;
            ViewData["id"] = "sek";
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
            if (ModelState.IsValid)
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
            return RedirectToAction("DodajNovuVjezbu", "Home");
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
            if (ModelState.IsValid)
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
            return RedirectToAction("IzmijeniVjezbu", "Home", vjp);
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

            //provjeri da li je ovo istezanje ukljuceno u neki trening i vrati id clana
            var istezanja = from x in _context.Istezanje
                               join i in _context.IstezanjePopis on x.IstezanjePopisIstezanjeId equals i.IstezanjeId
                               where i.IstezanjeId == id
                               select x.IstezanjePopisIstezanjeId;

            var provjeriTrening = from t in _context.Trening
                                  join z in _context.Istezanje on t.TreningId equals z.TreningId
                                  where z.IstezanjePopisIstezanjeId == istezanja.FirstOrDefault()
                                  group t by t.Clan into grp
                                  select grp.Key;

            List<String> clanovi = new List<string>();

            foreach (Clan c in provjeriTrening.ToList())
            {
                clanovi.Add(c.Ime + " " + c.Prezime + ", ");
            }
            //iduca linija je provjera koja ne brise istezanje ako postoji trening sa tin istezanjem
            if (clanovi.Count > 0)
            {
                string joined = String.Concat(clanovi.ToArray());
                return Content("<script language='javascript' type='text/javascript'>alert('Ovi clanovi koriste istezanje:" + joined + "');"
                    + "window.location.href='../IstezanjePopis';</script>");
            }
            else
            {
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
        }

        public ActionResult DetaljiIstezanja(int id = 0, int izmijeni = 0, int trening = 0, int treningId = 0, int counter = 0)
        {
            var ip = _context.IstezanjePopis.Find(id);

            if (izmijeni == 1)
            {
                ViewData["izmijeni"] = izmijeni;
            }
            if (trening == 1)
            {
                ViewData["trening"] = trening;
            }
            if (treningId > 0)
            {
                ViewData["treningId"] = treningId;
            }

            ViewData["counter"] = counter;
            ViewData["id"] = "ist";
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
            if (ModelState.IsValid)
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
            return RedirectToAction("DodajNovoIstezanje", "Home");
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
            if (ModelState.IsValid)
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
            return RedirectToAction("IzmijeniIstezanje", "Home", ip);
        }

        /****************Akcije sa clanovima i testovima****************/

        [HttpGet]
        public ActionResult DodajClana()
        {
            var c = new Clan();
            return View(c);
        }

        [HttpPost]
        public ActionResult DodajClana(Clan c, HttpPostedFileBase[] slike)
        {
            if (ModelState.IsValid)
            {
                if (slike != null && slike.FirstOrDefault() != null)
                {
                    var path = Server.MapPath("~/Content/ClanSlike/");
                    foreach (var file in slike)
                    {
                        var slika = new ClanSlike();
                        file.SaveAs(path + file.FileName);

                        slika.ClanSlikaIme = file.FileName;
                        slika.ClanClanId = c.ClanId;

                        c.ClanSlike.Add(slika);
                        _context.ClanSlike.Add(slika);
                    }
                }

                var age = (short) ((DateTime.Now - c.GodinaRodenja).TotalDays/365.242199);
                c.GodineStarosti = age;
                _context.Clan.Add(c);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("DodajClana");
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
                        join v in _context.SekcijaVjezbi on x.TreningId equals v.TreningId
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
                        foreach (Vjezba vj in b.Vjezba.ToList())
                        {
                            _context.Vjezba.Remove(vj);
                        }
                        _context.SekcijaVjezbi.Remove(b);
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
                    foreach (Slika sl in a.Slika.ToList())
                    {
                        _context.Slika.Remove(sl);
                    }
                    foreach (FunkcionalniRezultatiTest fn in a.FunkcionalniRezultatiTest.ToList())
                    {
                        _context.FunkcionalniRezultatiTest.Remove(fn);
                    }
                    foreach (MotorickiRezultatiTest mt in a.MotorickiRezultatiTest.ToList())
                    {
                        _context.MotorickiRezultatiTest.Remove(mt);
                    }
                    _context.Test.Remove(a);
                }
            }

            var queryAntropometrija = from x in _context.Antropometrija
                        where x.ClanClanId == id
                        select x;

            if (queryAntropometrija.FirstOrDefault() != null)
            {
                foreach (var ant in queryAntropometrija)
	                {
                        _context.Antropometrija.Remove(ant);
	                }
            }

            var queryAmneza = from x in _context.Amneza
                                      where x.ClanClanId == id
                                      select x;

            if (queryAmneza.FirstOrDefault() != null)
            {
                foreach (var amn in queryAmneza)
                {
                    _context.Amneza.Remove(amn);
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
        public ActionResult IzmijeniClana(Clan c, HttpPostedFileBase[] slike)
        {
            if (ModelState.IsValid)
            {
                if (slike != null && slike.FirstOrDefault() != null)
                {
                    var path = Server.MapPath("~/Content/ClanSlike/");
                    foreach (var file in slike)
                    {
                        var slika = new ClanSlike();
                        file.SaveAs(path + file.FileName);

                        slika.ClanSlikaIme = file.FileName;
                        slika.ClanClanId = c.ClanId;

                        c.ClanSlike.Add(slika);
                        _context.ClanSlike.Add(slika);
                    }
                }
                var age = (short) ((DateTime.Now - c.GodinaRodenja).TotalDays/365.242199);
                c.GodineStarosti = age;
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("IzmijeniClana", c);
        }        

        [HttpGet]
        public ActionResult DetaljiClana(int id = 0)
        {
            var c = _context.Clan.Find(id);

            var queryTest = from x in _context.Test
                where x.ClanId == id
                select x;

            var queryAntropometrija = from x in _context.Antropometrija
                            where x.ClanClanId == id
                            select x;

            var ctm = new ClanTestModel { Clan = c, ListaTest = queryTest.ToList(), ListAntropometrija = queryAntropometrija.ToList()};

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
        public ActionResult Amneza(int id = 0)
        {
            var query = from x in _context.Amneza
                        where x.Clan.ClanId == id
                        select x;

            var query1 = from x in _context.Clan
                         where x.ClanId == id
                         select x;

            var c = query1.Single();

            var amneza = new Amneza { Clan = c };

            return View(amneza);
        }

        [HttpGet]
        public ActionResult DodajAmnezu(int id = 0)
        {
            var a = new Amneza { ClanClanId = id };
            return View(a);
        }

        [HttpPost]
        public ActionResult DodajAmnezu(Amneza a, HttpPostedFileBase[] slike)
        {
            if (ModelState.IsValid)
            {
                if (slike != null && slike.FirstOrDefault() != null)
                {
                    var path = Server.MapPath("~/Content/AmnezaSlike/");
                    foreach (var file in slike)
                    {
                        var slika = new AmnezaSlike();
                        file.SaveAs(path + file.FileName);

                        slika.SlikaIme = file.FileName;
                        slika.AmnezaAmnezaId = a.AmnezaId;

                        a.AmnezaSlike.Add(slika);
                        _context.AmnezaSlike.Add(slika);
                    }
                }
                _context.Amneza.Add(a);
                _context.SaveChanges();
                return RedirectToAction("Amneza", new { id = a.ClanClanId });
            }
            return RedirectToAction("DodajAmnezu");
        }

        [HttpGet]
        public ActionResult IzmijeniAmnezu(int id = 0)
        {
            var a = _context.Amneza.Find(id);
            return View(a);
        }

        [HttpPost]
        public ActionResult IzmijeniAmnezu(Amneza a, HttpPostedFileBase[] slike)
        {
            if (ModelState.IsValid)
            {
                if (slike != null && slike.FirstOrDefault() != null)
                {
                    var path = Server.MapPath("~/Content/AmnezaSlike/");
                    foreach (var file in slike)
                    {
                        var slika = new AmnezaSlike();
                        file.SaveAs(path + file.FileName);

                        slika.SlikaIme = file.FileName;
                        slika.AmnezaAmnezaId = a.AmnezaId;

                        a.AmnezaSlike.Add(slika);
                        _context.AmnezaSlike.Add(slika);
                    }
                }

                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Amneza", new { id = a.ClanClanId });
            }
            return RedirectToAction("IzmijeniAmnezu", a);
        }

        public ActionResult IzbrisiAmnezu(int id)
        {
            var t = _context.Amneza.Find(id);

            if (t.AmnezaSlike.Count > 0)
            {
                var query = from x in _context.Amneza
                            join i in _context.AmnezaSlike on x.AmnezaId equals i.AmnezaAmnezaId
                            where x.AmnezaId == id
                            select i;

                foreach (var a in query.ToList())
                {
                    _context.AmnezaSlike.Remove(a);
                }
            }
            _context.Amneza.Remove(t);
            _context.SaveChanges();

            return RedirectToAction("Amneza", new { id = t.ClanClanId });
        }

        public ActionResult DetaljiAmneze(int id)
        {
            var t = _context.Amneza.Find(id);
            return View(t);

        }

        [HttpGet]
        public ActionResult AntropometrijaPopis(int id = 0)
        {
            var query = from x in _context.Antropometrija
                        where x.ClanClanId == id
                        select x;

            var query1 = from x in _context.Clan
                         where x.ClanId == id
                         select x;

            var c = query1.Single();

            var listaT = new AntropometrijaList { Clan = c };

            if (query.FirstOrDefault() != null)
            {
                listaT.AntropometrijaLista = query.ToList();
            }

            return View(listaT);
        }

        [HttpGet]
        public ActionResult DodajAntropometriju(int id = 0)
        {
            var a = new Antropometrija { ClanClanId = id };
            return View(a);
        }

        [HttpPost]
        public ActionResult DodajAntropometriju(Antropometrija a)
        {
            if (ModelState.IsValid)
            {
                _context.Antropometrija.Add(a);
                _context.SaveChanges();
                return RedirectToAction("AntropometrijaPopis", new { id = a.ClanClanId });
            }
            return RedirectToAction("DodajAntropometriju");
        }

        [HttpGet]
        public ActionResult IzmijeniAntropometriju(int id = 0)
        {
            var a = _context.Antropometrija.Find(id);
            return View(a);
        }

        [HttpPost]
        public ActionResult IzmijeniAntropometriju(Antropometrija a)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("AntropometrijaPopis", new { id = a.ClanClanId });
            }
            return RedirectToAction("IzmijeniAntropometriju", a);
        }

        public ActionResult IzbrisiAntropometriju(int id)
        {
            var a = _context.Antropometrija.Find(id);

            _context.Antropometrija.Remove(a);
            _context.SaveChanges();

            return RedirectToAction("AntropometrijaPopis", new { id = a.ClanClanId });
        }

        public ActionResult DetaljiAntropometrije(int id)
        {
            var a = _context.Antropometrija.Find(id);
            return View(a);

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
            if (ModelState.IsValid)
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
                return RedirectToAction("Test", new { id = t.ClanId });
            }
            return RedirectToAction("DodajTest");
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
            if (ModelState.IsValid)
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
            return RedirectToAction("IzmijeniTest", t);
        }

        public ActionResult DetaljiTest(int id)
        {
            var t = _context.Test.Find(id);
            return View(t);

        }

        [HttpGet]
        public ActionResult DodajFunkcionalniRezultat(int id = 0)
        {
            FunkcionalniRezultatiTest ft = new FunkcionalniRezultatiTest(){TestId = id};
            return View(ft);
        }

        [HttpPost]
        public ActionResult DodajFunkcionalniRezultat(FunkcionalniRezultatiTest ft)
        {
            _context.FunkcionalniRezultatiTest.Add(ft);
            _context.SaveChanges();

            return RedirectToAction("IzmijeniTest", new { id = ft.TestId });
        }

        [HttpGet]
        public ActionResult IzmijeniFunkcionalniRezultat(int id = 0)
        {
            var fn = _context.FunkcionalniRezultatiTest.Find(id);
            return View(fn);
        }

        [HttpPost]
        public ActionResult IzmijeniFunkcionalniRezultat(FunkcionalniRezultatiTest fn)
        {
            _context.Entry(fn).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("IzmijeniTest", new { id = fn.TestId });
        }

        public ActionResult IzbrisiFunkcionalniRezultat(int id)
        {
            var fn = _context.FunkcionalniRezultatiTest.Find(id);

            _context.FunkcionalniRezultatiTest.Remove(fn);
            _context.SaveChanges();

            return RedirectToAction("IzmijeniTest", new { id = fn.TestId });
        }

        [HttpGet]
        public ActionResult DodajMotorickiRezultat(int id = 0)
        {
            MotorickiRezultatiTest mt = new MotorickiRezultatiTest() { TestId = id };
            return View(mt);
        }

        [HttpPost]
        public ActionResult DodajMotorickiRezultat(MotorickiRezultatiTest mt)
        {
            _context.MotorickiRezultatiTest.Add(mt);
            _context.SaveChanges();

            return RedirectToAction("IzmijeniTest", new { id = mt.TestId });
        }

        [HttpGet]
        public ActionResult IzmijeniMotorickiRezultat(int id = 0)
        {
            var mt = _context.MotorickiRezultatiTest.Find(id);
            return View(mt);
        }

        [HttpPost]
        public ActionResult IzmijeniMotorickiRezultat(MotorickiRezultatiTest mt)
        {
            _context.Entry(mt).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("IzmijeniTest", new { id = mt.TestId });
        }

        public ActionResult IzbrisiMotorickiRezultat(int id)
        {
            var mt = _context.MotorickiRezultatiTest.Find(id);

            _context.MotorickiRezultatiTest.Remove(mt);
            _context.SaveChanges();

            return RedirectToAction("IzmijeniTest", new { id = mt.TestId });
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
                        join v in _context.SekcijaVjezbi on x.TreningId equals v.TreningId
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

                    foreach (SekcijaVjezbi a in query2.ToList())
                    {
                        foreach (Vjezba vj in a.Vjezba.ToList())
                        {
                            _context.Vjezba.Remove(vj);
                        }
                        _context.SekcijaVjezbi.Remove(a);
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
                join v in _context.SekcijaVjezbi on x.TreningId equals v.TreningId
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
                    foreach (Vjezba vj in a.Vjezba.ToList())
                    {
                        _context.Vjezba.Remove(vj);
                    }
                    _context.SekcijaVjezbi.Remove(a);
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
        public ActionResult IzmijeniTrening(int id = 0)
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
                join v in _context.SekcijaVjezbi on x.TreningId equals v.TreningId
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
                        TreningImeTreninga = firstOrDefault.x.ImeTreninga,
                        TreningTip = firstOrDefault.x.TipTreninga != null ? firstOrDefault.x.TipTreninga : null,
                        Napomena = firstOrDefault.x.Napomena,
                        TreningId = firstOrDefault.x.TreningId,
                        NapomenaZagrijavanje = firstOrDefault.x.NapomenaZagrijavanje,
                        NapomenaVjezba = firstOrDefault.x.NapomenaVjezba,
                        NapomenaIstezanje = firstOrDefault.x.NapomenaIstezanje
                    };

                    if (query1.FirstOrDefault() != null)
                    {
                        trm.ListaZagrijavanja = query1.ToList();
                    }
                    if (query2.FirstOrDefault() != null)
                    {
                        trm.SekcijaVjezbi = query2.ToList();
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
            tr.TipTreninga = trm.TreningTip != null ? trm.TreningTip : null;
            tr.DatumTreninga = trm.TreningDatum;
            tr.Napomena = trm.Napomena;
            tr.NapomenaZagrijavanje = trm.NapomenaZagrijavanje;
            tr.NapomenaVjezba = trm.NapomenaVjezba;
            tr.NapomenaIstezanje = trm.NapomenaIstezanje;

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
                join v in _context.SekcijaVjezbi on x.TreningId equals v.TreningId
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
                        TreningImeTreninga = firstOrDefault.x.ImeTreninga,
                        TreningTip = firstOrDefault.x.TipTreninga != null ? firstOrDefault.x.TipTreninga : null,
                        TreningId = firstOrDefault.x.TreningId,
                        Napomena = firstOrDefault.x.Napomena,
                        NapomenaZagrijavanje = firstOrDefault.x.NapomenaZagrijavanje,
                        NapomenaVjezba = firstOrDefault.x.NapomenaVjezba,
                        NapomenaIstezanje = firstOrDefault.x.NapomenaIstezanje
                    };

                    if (query1.FirstOrDefault() != null)
                    {
                        trm.ListaZagrijavanja = query1.ToList();
                    }
                    if (query2.FirstOrDefault() != null)
                    {
                        trm.SekcijaVjezbi = query2.ToList();
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

                var query2 = from x in _context.SekcijaVjezbi
                    join y in _context.Trening on x.TreningId equals y.TreningId
                    where y.TreningId == id
                    select x;

                trm.SekcijaVjezbi = query2.ToList();

                var query1 = from x in _context.Clan
                    join y in _context.Trening on x.ClanId equals y.ClanId
                    where y.TreningId == id
                    select x;

                var c = query1.Single();
                trm.ClanIme = c.Ime;
                trm.ClanPrezime = c.Prezime;
                trm.ClanId = c.ClanId;
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

                var query2 = from x in _context.SekcijaVjezbi
                    join y in _context.Trening on x.TreningId equals y.TreningId
                    where y.TreningId == id
                    select x;

                trm.SekcijaVjezbi = query2.ToList();

                var query1 = from x in _context.Clan
                    join y in _context.Trening on x.ClanId equals y.ClanId
                    where y.TreningId == id
                    select x;

                var c = query1.Single();
                trm.ClanIme = c.Ime;
                trm.ClanPrezime = c.Prezime;
                trm.ClanId = c.ClanId;
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
            tr.DatumTreninga = trm.TreningDatum;
            tr.Napomena = trm.Napomena;
            tr.NapomenaZagrijavanje = trm.NapomenaZagrijavanje;
            tr.NapomenaVjezba = trm.NapomenaVjezba;
            tr.NapomenaIstezanje = trm.NapomenaIstezanje;

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
        public ActionResult DodajVjezbuTrening(int id, string id1, int izmijeni = 0, int counter = 0)
        {
            var vj = new VjezbePopisLista
            {
                VjezbePopis = _context.VjezbePopis.ToList(),
                SekcijaId = id,
                Izmijeni = izmijeni
            };
            ViewData["izmijeni"] = izmijeni;
            ViewData["id1"] = id1;
            ViewData["counter"] = counter;
            ViewData["id"] = "sek";
            return View(vj);
        }

        [HttpPost]
        public ActionResult DodajVjezbuTrening(int vjpId = 0, int id = 0, int vjpIzmijeni = 0, int counter = 0)
        {
            var query = from x in _context.VjezbePopis
                where x.VjezbeId == vjpId
                select x;

            var vjp = query.Single();

            var vj = new Vjezba {ImeVjezbe = vjp.ImeVjezbe, SekcijaId = id, VjezbePopisVjezbeId = vjpId};
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
            var sekcija = _context.SekcijaVjezbi.Find(id);
            var trening = _context.Trening.Find(sekcija.TreningId);
            id = trening.TreningId;

            ViewData["izmijeni"] = vjpIzmijeni;
            ViewData["counter"] = counter;
            ViewData["id"] = "sek";
            return vjpIzmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new {id})
                : RedirectToAction("DodajTrening", new {id, DodajVjezbu = 2});
        }

        [HttpGet]
        public ActionResult IzbrisiVjezbuTrening(int id = 0, int izmijeni = 0)
        {
            var vj = _context.Vjezba.Find(id);
            var sec = _context.SekcijaVjezbi.Find(vj.SekcijaId);
            id = sec.TreningId;

            _context.Vjezba.Remove(vj);
            _context.SaveChanges();

            return izmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new {id})
                : RedirectToAction("DodajTrening", new {id, DodajVjezbu = 2});
        }

        /*Istezanje*/

        [HttpGet]
        public ActionResult DodajIstezanjeTrening(int id, int izmijeni = 0, int counter = 0)
        {
            var istp = new IstezanjeZagrijavanjeLista
            {
                IstezanjePopis = _context.IstezanjePopis.ToList(),
                TreningId = id,
                Izmijeni = izmijeni
            };
            ViewData["izmijeni"] = izmijeni;
            ViewData["counter"] = counter;
            ViewData["id"] = "ist";
            return View(istp);
        }

        [HttpPost]
        public ActionResult DodajIstezanjeTrening(int istpId = 0, int id = 0, int istpIzmijeni = 0, int counter = 0)
        {
            var query = from x in _context.IstezanjePopis
                where x.IstezanjeId == istpId
                select x;

            var istp = query.Single();

            var ist = new Istezanje {Naziv = istp.Naziv, TreningId = id, IstezanjePopisIstezanjeId = istpId};
            if (istp.Info != null)
            {
                ist.Info = istp.Info;
            }

            _context.Istezanje.Add(ist);
            _context.SaveChanges();

            ViewData["izmijeni"] = istpIzmijeni;
            ViewData["counter"] = counter;
            ViewData["id"] = "ist";
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

        /*Zagrijavanje*/

        [HttpGet]
        public ActionResult DodajZagrijavanjeTrening(int id, int izmijeni = 0, int counter = 0)
        {
            var zgp = new IstezanjeZagrijavanjeLista
            {
                ZagrijavanjePopis = _context.ZagrijavanjePopis.ToList(),
                TreningId = id,
                Izmijeni = izmijeni
            };
            ViewData["izmijeni"] = izmijeni;
            ViewData["counter"] = counter;
            return View(zgp);
        }

        [HttpPost]
        public ActionResult DodajZagrijavanjeTrening(int zgpId = 0, int id = 0, int zgpIzmijeni = 0, int counter = 0)
        {
            var query = from x in _context.ZagrijavanjePopis
                where x.ZagrijavanjeId == zgpId
                select x;

            var zgp = query.Single();

            var zg = new Zagrijavanje {Naziv = zgp.Naziv, TreningId = id, ZagrijavanjePopisZagrijavanjeId = zgpId};
            if (zgp.Info != null)
            {
                zg.Info = zgp.Info;
            }

            _context.Zagrijavanje.Add(zg);
            _context.SaveChanges();
            ViewData["izmijeni"] = zgpIzmijeni;
            ViewData["counter"] = counter;
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

        [HttpPost]
        public ActionResult SpremiZagrijavanjeInfo(string puls, string tempo = null, string Trajanje = null, string ZagrijavanjeNapomena = null, int id = 0,
            int ZagrijavanjeId = 0, int izmijeni = 0)
        {
            var query = from x in _context.Zagrijavanje
                where x.ZagrijavanjeId == ZagrijavanjeId
                select x;

            var zg = query.Single();
            zg.Tempo = tempo;
            zg.Puls = puls;
            zg.Trajanje = Trajanje;
            zg.ZagrijavanjeNapomena = ZagrijavanjeNapomena;

            _context.Entry(zg).State = EntityState.Modified;
            _context.SaveChanges();

            return izmijeni != 0
               ? RedirectToAction("IzmijeniTrening", new { id })
               : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpPost]
        public ActionResult SpremiVjezbuInfo(string brojPonavljanja, string brojSerija = null, string tezina = null,
            string odmor = null, int id = 0, int vjezbaId = 0, int izmijeni = 0)
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

            return izmijeni != 0
               ? RedirectToAction("IzmijeniTrening", new { id })
               : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpPost]
        public ActionResult SpremiIstezanjeInfo(string vrijemeIzdrzaja, string vrstaIstezanja = null, int id = 0,
            int istezanjeId = 0, int izmijeni = 0)
        {
            var query = from x in _context.Istezanje
                where x.IstezanjeId == istezanjeId
                select x;

            var ist = query.Single();
            ist.VrijemeIzdrzaja = vrijemeIzdrzaja;
            ist.VrstaIstezanja = vrstaIstezanja;

            _context.Entry(ist).State = EntityState.Modified;
            _context.SaveChanges();

            return izmijeni != 0
               ? RedirectToAction("IzmijeniTrening", new { id })
               : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
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

        public ActionResult IzbrisiSlikuClana(int id, int? slika)
        {
            var t = _context.Clan.Find(id);
            var imageToDelete = _context.ClanSlike.Find(slika);

            _context.ClanSlike.Remove(imageToDelete);
            _context.SaveChanges();

            t.ClanSlike.Remove(imageToDelete);
            _context.SaveChanges();

            return View("IzmijeniClana", t);
        }

        public ActionResult IzbrisiSlikuAmneze(int id, int? slika)
        {
            var t = _context.Amneza.Find(id);
            var imageToDelete = _context.AmnezaSlike.Find(slika);

            _context.AmnezaSlike.Remove(imageToDelete);
            _context.SaveChanges();

            t.AmnezaSlike.Remove(imageToDelete);
            _context.SaveChanges();

            return View("IzmijeniAmnezu", t);
        }

        /*Akcije se sekcijom vjezbi u treningu */
        public ActionResult DodajSekcijuTrening(int id, int izmijeni = 0)
        {
            var sekcija = new SekcijaVjezbi { TreningId = id };

            _context.SekcijaVjezbi.Add(sekcija);
            _context.SaveChanges();

            return izmijeni != 0
               ? RedirectToAction("IzmijeniTrening", new { id })
               : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }
        public ActionResult IzbrisiSekcijuVjezbi(int id, int izmijeni = 0)
        {
            var query = from x in _context.SekcijaVjezbi
                        where x.SekcijaId == id
                        select x;

            SekcijaVjezbi sekcija = query.Single();
            id = sekcija.TreningId;

            if (sekcija.Vjezba != null)
            {
                foreach (var a in sekcija.Vjezba.ToList())
                {
                    _context.Vjezba.Remove(a);
                }
            }

            _context.SekcijaVjezbi.Remove(sekcija);
            _context.SaveChanges();

            return izmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpPost]
        public ActionResult SpremiBrojKrugova(string BrojKrugova = null, string odmor = null, int id = 0, int sekcijaId = 0, int izmijeni = 0)
        {
            var query = from x in _context.SekcijaVjezbi
                        where x.SekcijaId == sekcijaId
                        select x;

            var sekcija = query.Single();
            sekcija.BrojKrugova = BrojKrugova;
            sekcija.Odmor = odmor;

            _context.Entry(sekcija).State = EntityState.Modified;
            _context.SaveChanges();

            return izmijeni != 0
               ? RedirectToAction("IzmijeniTrening", new { id })
               : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
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