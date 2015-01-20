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

        #region Zagrijavanje
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

            //provjeri dali je ovo zagrijavanje ukljuceno u neku skupinu i vrati id skupine
            var zagrijavanja = from x in _context.Zagrijavanje
                               join i in _context.ZagrijavanjePopis on x.ZagrijavanjeId equals i.ZagrijavanjeId
                               where i.ZagrijavanjeId == id
                               select x.ZagrijavanjeSkupinaZagrijavanjeSkupinaId;

            //vrati id od treninga u koji je ukljucena skupina
            var skupina = from x in _context.ZagrijavanjeSkupina
                          where x.ZagrijavanjeSkupinaId == zagrijavanja.FirstOrDefault()
                          select x.TreningTreningId;

            //provjeri dali je ovo zagrijavanje ukljuceno u neku sekciju i vrati id sekcije
            var zagrijavanjaSekcija = from x in _context.ZagrijavanjeVjezbaSet
                                      join i in _context.ZagrijavanjePopis on x.ZagrijavanjePopisZagrijavanjeId1 equals i.ZagrijavanjeId
                                      where i.ZagrijavanjeId == id
                                      select x.SekcijaVjezbiSekcijaId;

            //vrati id od treninga u koji je ukljucena sekcija
            var sekcija = from x in _context.SekcijaVjezbi
                          where x.SekcijaId == zagrijavanjaSekcija.FirstOrDefault()
                          select x.TreningId;

            //vrati clana koji koristi trening u koji je ukljuceno zagrijavanje
            var provjeriTrening = from t in _context.Trening
                                   join z in _context.SekcijaVjezbi on t.TreningId equals z.TreningId
                                   join a in _context.ZagrijavanjeSkupina on t.TreningId equals a.TreningTreningId
                                   where t.TreningId == sekcija.FirstOrDefault() || t.TreningId == skupina.FirstOrDefault()
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

                //provjeri da li je ovo ZagrijavanjePopis vezano za neko Zagrijavanje i update sva Zagrijavanje (Trening dio)
                var zagrijavanja = from x in _context.Zagrijavanje
                                join z in _context.ZagrijavanjePopis on x.ZagrijavanjePopisZagrijavanjeId equals z.ZagrijavanjeId
                                where z.ZagrijavanjeId == zp.ZagrijavanjeId
                                select x;
                foreach (Zagrijavanje z in zagrijavanja.ToList())
                {
                    z.Naziv = zp.Naziv;
                    z.Info = zp.Info;
                    _context.Entry(z).State = EntityState.Modified;
                }

                //provjeri da li je ovo ZagrijavanjePopis vezano za neko ZagrijavanjeVjezba i update sva Zagrijavanje (Trening dio)
                var zagrijavanjaVjezbe = from x in _context.ZagrijavanjeVjezbaSet
                                         join z in _context.ZagrijavanjePopis on x.ZagrijavanjePopisZagrijavanjeId1 equals z.ZagrijavanjeId
                                         where z.ZagrijavanjeId == zp.ZagrijavanjeId
                                         select x;
                foreach (ZagrijavanjeVjezba z in zagrijavanjaVjezbe.ToList())
                {
                    z.Naziv = zp.Naziv;
                    z.Info = zp.Info;
                    _context.Entry(z).State = EntityState.Modified;
                }

                _context.Entry(zp).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("ZagrijavanjePopis", "Home");
            }
            return RedirectToAction("IzmijeniZagrijavanje", zp);
        }
        #endregion Zagrijavanje

        #region Vjezbe
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

            //nadji id od ove vjezbe ako je ukljucena u zagrijavanje
            var vj = from x in _context.VjezbaZagrijavanjeSet
                     join i in _context.VjezbePopis on x.VjezbePopisVjezbeId equals i.VjezbeId
                     where x.VjezbePopisVjezbeId == id
                     select x.VjezbaId;            

            //provjeri da li je ova vjezba ukljucena u neku sekciju i vrati id od sekcije
            var vjezba = from x in _context.Vjezba
                         join v in _context.VjezbePopis on x.VjezbePopisVjezbeId equals v.VjezbeId
                         where v.VjezbeId == id
                         select x.SekcijaId;

            List<Clan> provjeriTreningupdejtanalista = new List<Clan>();
            IQueryable<Clan> provjeriTrening;
            IQueryable<Clan> provjeriTreningA;

            if (vj.Any())
            {
                foreach (var v in vj.ToList())
                {
                    //provjeri dali je ova vjezba ukljucena u neku skupinu zagrijavanja
                    var zagrijavanje = from x in _context.VjezbaZagrijavanjeSet
                                       where x.VjezbaId == v
                                       select x.ZagrijavanjeSkupinaZagrijavanjeSkupinaId;

                    //vrati id od treninga u koji je ukljucena ova skupina
                    var skupina = from x in _context.ZagrijavanjeSkupina
                                  where x.ZagrijavanjeSkupinaId == zagrijavanje.FirstOrDefault()
                                  select x.TreningTreningId;

                    foreach (var sk in skupina.ToList())
                    {
                        provjeriTreningA = from t in _context.Trening
                                           join a in _context.ZagrijavanjeSkupina on t.TreningId equals a.TreningTreningId
                                           where t.TreningId == sk
                                           group t by t.Clan into grp
                                           select grp.Key;

                        if (!provjeriTreningupdejtanalista.Contains(provjeriTreningA.FirstOrDefault()))
                        {
                            provjeriTreningupdejtanalista.Add(provjeriTreningA.FirstOrDefault());
                        }
                    }      
                }
            }

            if (vjezba.Any())
            {
                foreach (var s in vjezba.ToList())
                {
                    //vrati id od treninga u koji je ukljucena ova sekcija
                    var sekcija = from x in _context.SekcijaVjezbi
                                  where x.SekcijaId == s
                                  select x.TreningId;

                    //vrati clana koji koristi trening u koji je ukljucena vjezba
                    provjeriTrening = from t in _context.Trening
                                      join se in _context.SekcijaVjezbi on t.TreningId equals se.TreningId
                                      where se.SekcijaId == s
                                      group t by t.Clan into grp
                                      select grp.Key;

                    if (!provjeriTreningupdejtanalista.Contains(provjeriTrening.FirstOrDefault()))
                    {
                        provjeriTreningupdejtanalista.Add(provjeriTrening.FirstOrDefault());
                    }
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

                //provjeri da li je ova VjezbePopis vezana za neku Vjezba i update sve Vjezba (Trening dio)
                var vjezbe = from x in _context.Vjezba
                             join v in _context.VjezbePopis on x.VjezbePopisVjezbeId equals v.VjezbeId
                             where v.VjezbeId == vjp.VjezbeId
                             select x;
                foreach (Vjezba v in vjezbe.ToList())
                {
                    v.ImeVjezbe = vjp.ImeVjezbe;
                    v.Info = vjp.Info;
                    _context.Entry(v).State = EntityState.Modified;
                }

                //provjeri da li je ova VjezbePopis vezana za neku VjezbaZagrijavanje i update sve VjezbaZagrijavanje (Trening dio)
                var vjezbeZagrijavanje = from x in _context.VjezbaZagrijavanjeSet
                                         join v in _context.VjezbePopis on x.VjezbePopisVjezbeId equals v.VjezbeId
                                         where v.VjezbeId == vjp.VjezbeId
                                         select x;
                foreach (VjezbaZagrijavanje v in vjezbeZagrijavanje.ToList())
                {
                    v.ImeVjezbe = vjp.ImeVjezbe;
                    v.Info = vjp.Info;
                    _context.Entry(v).State = EntityState.Modified;
                }

                _context.Entry(vjp).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("VjezbePopis", "Home");
            }
            return RedirectToAction("IzmijeniVjezbu", "Home", vjp);
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
        #endregion Vjezbe

        #region AerobneVjezbe
        [HttpGet]
        public ActionResult AerobneVjezbePopis()
        {
            var listaAerobnihVjezbi = _context.AerobneVjezbePopis.ToList();
            if (listaAerobnihVjezbi.Count == 0)
            {
                listaAerobnihVjezbi = new List<AerobneVjezbePopis>
                {
                    new AerobneVjezbePopis {AerobnaVjezbaId = 0, Naziv = "", Info = "", AerobneVjezbeSlike = null}
                };
                return View(listaAerobnihVjezbi);
            }
            return View(listaAerobnihVjezbi);
        }

        [HttpGet]
        public ActionResult DodajNovuAerobnuVjezbu()
        {
            var avjp = new AerobneVjezbePopis();
            return View(avjp);
        }

        [HttpPost]
        public ActionResult DodajNovuAerobnuVjezbu(AerobneVjezbePopis avjp, HttpPostedFileBase[] slike)
        {
            if (ModelState.IsValid)
            {
                if (slike != null && slike.FirstOrDefault() != null)
                {
                    var path = Server.MapPath("~/Content/VjezbeSlike/");
                    foreach (var file in slike)
                    {
                        var slika = new AerobneVjezbeSlike();
                        file.SaveAs(path + file.FileName);

                        slika.AerobnaVjezbaSlikaIme = file.FileName;
                        slika.AerobneVjezbePopisAerobnaVjezbaId = avjp.AerobnaVjezbaId;

                        avjp.AerobneVjezbeSlike.Add(slika);
                        _context.AerobneVjezbeSlike.Add(slika);
                    }
                }

                _context.AerobneVjezbePopis.Add(avjp);
                _context.SaveChanges();
                return RedirectToAction("AerobneVjezbePopis", "Home");
            }
            return RedirectToAction("DodajNovuAerobnuVjezbu", "Home");
        }

        [HttpGet]
        public ActionResult IzmijeniAerobnuVjezbu(int id = 0)
        {
            var ip = _context.AerobneVjezbePopis.Find(id);
            return View(ip);
        }

        [HttpPost]
        public ActionResult IzmijeniAerobnuVjezbu(AerobneVjezbePopis avjp, HttpPostedFileBase[] slike)
        {
            if (ModelState.IsValid)
            {
                if (slike != null && slike.FirstOrDefault() != null)
                {
                    var path = Server.MapPath("~/Content/VjezbeSlike/");

                    foreach (var file in slike)
                    {
                        var slika = new AerobneVjezbeSlike();
                        file.SaveAs(path + file.FileName);

                        slika.AerobnaVjezbaSlikaIme = file.FileName;
                        slika.AerobneVjezbePopisAerobnaVjezbaId = avjp.AerobnaVjezbaId;

                        avjp.AerobneVjezbeSlike.Add(slika);
                        _context.AerobneVjezbeSlike.Add(slika);
                    }
                }

                //provjeri da li je ova AerobneVjezbepopis vezana za neku AerobneVjezbe i update sve AerobneVjezbe (Trening dio)
                var aerobnevjezbe = from x in _context.AerobneVjezbe
                             join a in _context.AerobneVjezbePopis on x.AerobneVjezbePopisAerobnaVjezbaId equals a.AerobnaVjezbaId
                             where a.AerobnaVjezbaId == avjp.AerobnaVjezbaId
                             select x;
                foreach (AerobneVjezbe a in aerobnevjezbe.ToList())
                {
                    a.Naziv = avjp.Naziv;
                    a.Info = avjp.Info;
                    _context.Entry(a).State = EntityState.Modified;
                }

                _context.Entry(avjp).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("AerobneVjezbePopis", "Home");
            }
            return RedirectToAction("IzmijeniAerobnuVjezbu", "Home", avjp);
        }

        public ActionResult DetaljiAerobneVjezbe(int id = 0, int izmijeni = 0, int trening = 0, int treningId = 0, int counter = 0)
        {
            var avjp = _context.AerobneVjezbePopis.Find(id);

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
            return View(avjp);
        }

        public ActionResult IzbrisiAerobnuVjezbu(int id = 0)
        {
            var avjp = _context.AerobneVjezbePopis.Find(id);

            //provjeri da li je ova aerobna vjezba ukljucena u neki trening i vrati id clana
            var aerobnevjezbe = from x in _context.AerobneVjezbe
                                join a in _context.AerobneVjezbePopis on x.AerobneVjezbePopisAerobnaVjezbaId equals a.AerobnaVjezbaId
                                where a.AerobnaVjezbaId == id
                                select x.AerobneVjezbePopisAerobnaVjezbaId;

            var sekcije = from s in _context.SekcijaVjezbi
                          join a in _context.AerobneVjezbe on s.SekcijaId equals a.SekcijaVjezbiSekcijaId
                          where a.AerobneVjezbePopisAerobnaVjezbaId == aerobnevjezbe.FirstOrDefault()
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

                if (!provjeriTreningupdejtanalista.Contains(provjeriTrening.FirstOrDefault()))
                {
                    provjeriTreningupdejtanalista.Add(provjeriTrening.FirstOrDefault());
                }
            }

            List<String> clanovi = new List<string>();

            foreach (Clan c in provjeriTreningupdejtanalista)
            {
                clanovi.Add(c.Ime + " " + c.Prezime + ", ");
            }
            //iduca linija je provjera koja ne brise aerobne vjezbe ako postoji trening sa tom vjezbom
            if (clanovi.Count > 0)
            {
                string joined = String.Concat(clanovi.ToArray());
                return Content("<script language='javascript' type='text/javascript'>alert('Ovi clanovi koriste aerobnu vjezbu:" + joined + "');"
                    + "window.location.href='../AerobneVjezbePopis';</script>");
            }
            else 
            {
                if(avjp.AerobneVjezbeSlike.Count > 0)
                {
                    var query = from x in _context.AerobneVjezbePopis
                                join i in _context.AerobneVjezbeSlike on x.AerobnaVjezbaId equals i.AerobneVjezbePopisAerobnaVjezbaId
                                where x.AerobnaVjezbaId == id
                                select i;

                    foreach (var a in query.ToList())
                    {
                        _context.AerobneVjezbeSlike.Remove(a);
                    }
                }

            _context.AerobneVjezbePopis.Remove(avjp);
            _context.SaveChanges();
            return RedirectToAction("AerobneVjezbePopis", "Home");
            }
        }

        public ActionResult IzbrisiSlikuAerobneVjezbe(int id, int? slika)
        {
            var t = _context.AerobneVjezbePopis.Find(id);
            var imageToDelete = _context.AerobneVjezbeSlike.Find(slika);

            _context.AerobneVjezbeSlike.Remove(imageToDelete);
            _context.SaveChanges();

            t.AerobneVjezbeSlike.Remove(imageToDelete);
            _context.SaveChanges();

            return View("IzmijeniAerobnuVjezbu", t);
        }
        #endregion AerobneVjezbe

        #region AnaerobneVjezbe
        [HttpGet]
        public ActionResult AnaerobneVjezbePopis()
        {
            var listaAnaerobnihVjezbi = _context.AnaerobneVjezbePopis.ToList();
            if (listaAnaerobnihVjezbi.Count == 0)
            {
                listaAnaerobnihVjezbi = new List<AnaerobneVjezbePopis>
                {
                    new AnaerobneVjezbePopis {AnaerobnaVjezbaId = 0, Naziv = "", Info = "", AnaerobneVjezbeSlike = null}
                };
                return View(listaAnaerobnihVjezbi);
            }
            return View(listaAnaerobnihVjezbi);
        }

        [HttpGet]
        public ActionResult DodajNovuAnaerobnuVjezbu()
        {
            var avjp = new AnaerobneVjezbePopis();
            return View(avjp);
        }

        [HttpPost]
        public ActionResult DodajNovuAnaerobnuVjezbu(AnaerobneVjezbePopis avjp, HttpPostedFileBase[] slike)
        {
            if (ModelState.IsValid)
            {
                if (slike != null && slike.FirstOrDefault() != null)
                {
                    var path = Server.MapPath("~/Content/VjezbeSlike/");
                    foreach (var file in slike)
                    {
                        var slika = new AnaerobneVjezbeSlike();
                        file.SaveAs(path + file.FileName);

                        slika.AnaerobnaVjezbaSlikaIme = file.FileName;
                        slika.AnaerobneVjezbePopisAnaerobnaVjezbaId = avjp.AnaerobnaVjezbaId;

                        avjp.AnaerobneVjezbeSlike.Add(slika);
                        _context.AnaerobneVjezbeSlike.Add(slika);
                    }
                }

                _context.AnaerobneVjezbePopis.Add(avjp);
                _context.SaveChanges();
                return RedirectToAction("AnaerobneVjezbePopis", "Home");
            }
            return RedirectToAction("DodajNovuAnaerobnuVjezbu", "Home");
        }

        [HttpGet]
        public ActionResult IzmijeniAnaerobnuVjezbu(int id = 0)
        {
            var ip = _context.AnaerobneVjezbePopis.Find(id);
            return View(ip);
        }

        [HttpPost]
        public ActionResult IzmijeniAnaerobnuVjezbu(AnaerobneVjezbePopis avjp, HttpPostedFileBase[] slike)
        {
            if (ModelState.IsValid)
            {
                if (slike != null && slike.FirstOrDefault() != null)
                {
                    var path = Server.MapPath("~/Content/VjezbeSlike/");

                    foreach (var file in slike)
                    {
                        var slika = new AnaerobneVjezbeSlike();
                        file.SaveAs(path + file.FileName);

                        slika.AnaerobnaVjezbaSlikaIme = file.FileName;
                        slika.AnaerobneVjezbePopisAnaerobnaVjezbaId = avjp.AnaerobnaVjezbaId;

                        avjp.AnaerobneVjezbeSlike.Add(slika);
                        _context.AnaerobneVjezbeSlike.Add(slika);
                    }
                }

                //provjeri da li je ova AerobneVjezbepopis vezana za neku AerobneVjezbe i update sve AerobneVjezbe (Trening dio)
                var anaerobnevjezbe = from x in _context.AnaerobneVjezbe
                                      join an in _context.AnaerobneVjezbePopis on x.AnaerobneVjezbePopisAnaerobnaVjezbaId equals an.AnaerobnaVjezbaId
                                      where an.AnaerobnaVjezbaId == avjp.AnaerobnaVjezbaId
                                      select x;
                foreach (AnaerobneVjezbe an in anaerobnevjezbe.ToList())
                {
                    an.Naziv = avjp.Naziv;
                    an.Info = avjp.Info;
                    _context.Entry(an).State = EntityState.Modified;
                }

                _context.Entry(avjp).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("AnaerobneVjezbePopis", "Home");
            }
            return RedirectToAction("IzmijeniAnaerobnuVjezbu", "Home", avjp);
        }

        public ActionResult DetaljiAnaerobneVjezbe(int id = 0, int izmijeni = 0, int trening = 0, int treningId = 0, int counter = 0)
        {
            var avjp = _context.AnaerobneVjezbePopis.Find(id);

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
            return View(avjp);
        }

        public ActionResult IzbrisiAnaerobnuVjezbu(int id = 0)
        {
            var anvjp = _context.AnaerobneVjezbePopis.Find(id);

            //provjeri da li je ova vjezba ukljucena u neki trening i vrati id clana
            var anaerobnevjezbe = from x in _context.AnaerobneVjezbe
                                  join an in _context.AnaerobneVjezbePopis on x.AnaerobneVjezbePopisAnaerobnaVjezbaId equals an.AnaerobnaVjezbaId
                                  where an.AnaerobnaVjezbaId == id
                                  select x.AnaerobneVjezbePopisAnaerobnaVjezbaId;

            var sekcije = from s in _context.SekcijaVjezbi
                          join an in _context.AnaerobneVjezbe on s.SekcijaId equals an.SekcijaVjezbiSekcijaId
                          where an.AnaerobneVjezbePopisAnaerobnaVjezbaId == anaerobnevjezbe.FirstOrDefault()
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

                if (!provjeriTreningupdejtanalista.Contains(provjeriTrening.FirstOrDefault()))
                {
                    provjeriTreningupdejtanalista.Add(provjeriTrening.FirstOrDefault());
                }
            }

            List<String> clanovi = new List<string>();

            foreach (Clan c in provjeriTreningupdejtanalista)
            {
                clanovi.Add(c.Ime + " " + c.Prezime + ", ");
            }
            //iduca linija je provjera koja ne brise anaerobne vjezbe ako postoji trening sa tom vjezbom
            if (clanovi.Count > 0)
            {
                string joined = String.Concat(clanovi.ToArray());
                return Content("<script language='javascript' type='text/javascript'>alert('Ovi clanovi koriste anaerobnu vjezbu:" + joined + "');"
                    + "window.location.href='../AnaerobneVjezbePopis';</script>");
            }
            else
            {
                if (anvjp.AnaerobneVjezbeSlike.Count > 0)
                {
                    var query = from x in _context.AnaerobneVjezbePopis
                                join i in _context.AnaerobneVjezbeSlike on x.AnaerobnaVjezbaId equals i.AnaerobneVjezbePopisAnaerobnaVjezbaId
                                where x.AnaerobnaVjezbaId == id
                                select i;

                    foreach (var a in query.ToList())
                    {
                        _context.AnaerobneVjezbeSlike.Remove(a);
                    }
                }

                _context.AnaerobneVjezbePopis.Remove(anvjp);
                _context.SaveChanges();
                return RedirectToAction("AnaerobneVjezbePopis", "Home");
            }
        }

        public ActionResult IzbrisiSlikuAnaerobneVjezbe(int id, int? slika)
        {
            var t = _context.AnaerobneVjezbePopis.Find(id);
            var imageToDelete = _context.AnaerobneVjezbeSlike.Find(slika);

            _context.AnaerobneVjezbeSlike.Remove(imageToDelete);
            _context.SaveChanges();

            t.AnaerobneVjezbeSlike.Remove(imageToDelete);
            _context.SaveChanges();

            return View("IzmijeniAnaerobnuVjezbu", t);
        }
        #endregion AnaerobneVjezbe

        #region Istezanje
        [HttpGet]
        public ActionResult IstezanjePopis()
        {
            var listaIstezanja = _context.IstezanjePopis.ToList();
            if (listaIstezanja.Count == 0)
            {
                listaIstezanja = new List<IstezanjePopis> { new IstezanjePopis { IstezanjeId = 0, Naziv = "", Info = "" } };
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

            //provjeri da li je ovo istezanje ukljuceno u neki predlozak i vrati id templatea
            var istezanjaT = from x in _context.IstezanjeT
                             join i in _context.IstezanjePopis on x.IstezanjePopisIstezanjeId equals i.IstezanjeId
                             where i.IstezanjeId == id
                             select x.IstezanjePopisIstezanjeId;

            var provjeriPredlozak = from p in _context.IstezanjeTreningTemplate
                                    join z in _context.IstezanjeT on p.IstezanjeTreningTemplateId equals z.IstezanjeTreningTemplateIstezanjeTreningTemplateId
                                    where z.IstezanjePopisIstezanjeId == istezanjaT.FirstOrDefault()
                                    group p by p into grp
                                    select grp.Key;

            List<String> predlosci = new List<string>();

            foreach (IstezanjeTreningTemplate itt in provjeriPredlozak.ToList())
            {
                predlosci.Add(itt.NazivPredloska + ", ");
            }

            //iduca linija je provjera koja ne brise istezanje ako postoji trening sa tin istezanjem
            if (clanovi.Count > 0)
            {
                string joined = String.Concat(clanovi.ToArray());
                return Content("<script language='javascript' type='text/javascript'>alert('Ovi clanovi koriste istezanje:" + joined + "');"
                    + "window.location.href='../IstezanjePopis';</script>");
            }
            else if (predlosci.Count > 0)
            {
                string joined = String.Concat(predlosci.ToArray());
                return Content("<script language='javascript' type='text/javascript'>alert('Ovi predlosci koriste istezanje:" + joined + "');"
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

        public ActionResult DetaljiIstezanja(int id = 0, int izmijeni = 0, int trening = 0, int treningId = 0, int counter = 0, int dodajPredlozak = 0, int izmijeniPredlozak = 0, int detaljiPredlozak = 0, int templateId = 0)
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
            if (dodajPredlozak == 1)
            {
                ViewData["dodajPredlozak"] = dodajPredlozak;
            }
            if (izmijeniPredlozak == 1)
            {
                ViewData["izmijeniPredlozak"] = izmijeniPredlozak;
            }
            if (detaljiPredlozak == 1)
            {
                ViewData["detaljiPredlozak"] = detaljiPredlozak;
            }
            if (templateId > 0)
            {
                ViewData["templateId"] = templateId;
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

                //provjeri da li je ovo IstezanjePopis vezano za neko Istezanje i update sva Istezanje (Trening dio)
                var istezanja = from x in _context.Istezanje
                                join i in _context.IstezanjePopis on x.IstezanjePopisIstezanjeId equals i.IstezanjeId
                                where i.IstezanjeId == ip.IstezanjeId
                                select x;
                foreach (Istezanje i in istezanja.ToList())
                {
                    i.Naziv = ip.Naziv;
                    i.Info = ip.Info;
                    _context.Entry(i).State = EntityState.Modified;
                }
                
                //provjeri da li je ovo IstezanjePopis vezano za neko IstezanjeT i update sva IstezanjeT (Predlozak dio)
                var istezanjaT = from x in _context.IstezanjeT
                                 join i in _context.IstezanjePopis on x.IstezanjePopisIstezanjeId equals i.IstezanjeId
                                 where i.IstezanjeId == ip.IstezanjeId
                                 select x;
                foreach (IstezanjeT i in istezanjaT.ToList())
                {
                    i.NazivT = ip.Naziv;
                    i.InfoT = ip.Info;
                    _context.Entry(i).State = EntityState.Modified;
                }

                _context.Entry(ip).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("IstezanjePopis", "Home");
            }
            return RedirectToAction("IzmijeniIstezanje", "Home", ip);
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

        #endregion Istezanje

        #region IstezanjePredlozak

        [HttpGet]
        public ActionResult PredlosciIstezanjePopis()
        {
            foreach (IstezanjeTreningTemplate ITT in _context.IstezanjeTreningTemplate.ToList())
            {
                if (ITT.NazivPredloska == "")
                {
                    var queryIst =  from x in _context.IstezanjeT
                                    join i in _context.IstezanjeTreningTemplate 
                                    on x.IstezanjeTreningTemplateIstezanjeTreningTemplateId equals i.IstezanjeTreningTemplateId
                                    where i.NazivPredloska == ""
                                    select x;

                    foreach (IstezanjeT Ist in queryIst)
                    {
                        _context.IstezanjeT.Remove(Ist);
                    }

                    _context.IstezanjeTreningTemplate.Remove(ITT);
                    _context.SaveChanges();
                }
            }

            var listaPredlozakaIstezanja = _context.IstezanjeTreningTemplate.ToList();

            if (listaPredlozakaIstezanja.Count == 0)
            {
                listaPredlozakaIstezanja = new List<IstezanjeTreningTemplate> { new IstezanjeTreningTemplate { IstezanjeTreningTemplateId = 0, NazivPredloska = "" } };
                return View(listaPredlozakaIstezanja);
            }

            return View(listaPredlozakaIstezanja);
        }

        public ActionResult IzbrisiPredlozakIstezanja(int id = 0)
        {
            var ITT = _context.IstezanjeTreningTemplate.Find(id);
            /*
            //provjeri da li je ovo istezanje ukljuceno u neki trening i vrati id clana
            var predlozakistezanja = from x in _context.Istezanje
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
                */

                var queryIst = from x in _context.IstezanjeT
                               join i in _context.IstezanjeTreningTemplate
                               on x.IstezanjeTreningTemplateIstezanjeTreningTemplateId equals i.IstezanjeTreningTemplateId
                               where i.IstezanjeTreningTemplateId == id
                               select x;

                foreach (IstezanjeT Ist in queryIst)
                {
                    _context.IstezanjeT.Remove(Ist);
                }

                _context.IstezanjeTreningTemplate.Remove(ITT);
                _context.SaveChanges();

                return RedirectToAction("PredlosciIstezanjePopis", "Home");
            }

        [HttpGet]
        public ActionResult IzmijeniPredlozakIstezanja(int id = 0)
        {
            var predlozakModel = new IstezanjeTemplateListaIstezanjaModel();

            var queryTemp = from t in _context.IstezanjeTreningTemplate
                            where t.IstezanjeTreningTemplateId == id
                            select t;

            var temp = queryTemp.Single();

            var queryIst = from i in _context.IstezanjeT
                           join t in _context.IstezanjeTreningTemplate
                           on i.IstezanjeTreningTemplateIstezanjeTreningTemplateId equals t.IstezanjeTreningTemplateId
                           where t.IstezanjeTreningTemplateId == id
                           select i;

            predlozakModel = new IstezanjeTemplateListaIstezanjaModel() { ITT = temp, ListaIstezanjeT = queryIst.ToList() };

            return View(predlozakModel);
        }

        [HttpPost]
        public ActionResult IzmijeniPredlozakIstezanja(IstezanjeTemplateListaIstezanjaModel predlozakModel)
        {
            var query = from x in _context.IstezanjeTreningTemplate
                        where x.IstezanjeTreningTemplateId == predlozakModel.ITT.IstezanjeTreningTemplateId
                        select x;

            var ITT = query.Single();

            ITT.NazivPredloska = predlozakModel.ITT.NazivPredloska;

            _context.Entry(ITT).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("PredlosciIstezanjePopis");
        }

        [HttpGet]
        public ActionResult DetaljiPredlozakIstezanja(int id = 0, int treningId = 0, int izmijeni = 0)
        {
            var predlozakModel = new IstezanjeTemplateListaIstezanjaModel();

            var queryTemp = from t in _context.IstezanjeTreningTemplate
                            where t.IstezanjeTreningTemplateId == id
                            select t;

            var temp = queryTemp.Single();

            var queryIst = from i in _context.IstezanjeT
                           join t in _context.IstezanjeTreningTemplate
                           on i.IstezanjeTreningTemplateIstezanjeTreningTemplateId equals t.IstezanjeTreningTemplateId
                           where t.IstezanjeTreningTemplateId == id
                           select i;

            ViewData["treningId"] = treningId;
            ViewData["izmijeni"] = izmijeni;

            predlozakModel = new IstezanjeTemplateListaIstezanjaModel() { ITT = temp, ListaIstezanjeT = queryIst.ToList() };

            return View(predlozakModel);
        }

        [HttpGet]
        public ActionResult DodajPredlozakIstezanja(int id = 0)
        {
            var predlozakModel = new IstezanjeTemplateListaIstezanjaModel();

            if (id != 0)
            {
                var queryTemp = from t in _context.IstezanjeTreningTemplate
                                where t.IstezanjeTreningTemplateId == id
                                select t;

                var temp = queryTemp.Single();

                var queryIst = from i in _context.IstezanjeT
                               join t in _context.IstezanjeTreningTemplate
                               on i.IstezanjeTreningTemplateIstezanjeTreningTemplateId equals t.IstezanjeTreningTemplateId
                               where t.IstezanjeTreningTemplateId == id
                               select i;

                predlozakModel = new IstezanjeTemplateListaIstezanjaModel() { ITT = temp, ListaIstezanjeT = queryIst.ToList() };
            }
            else
            {
                IstezanjeTreningTemplate ITT = new IstezanjeTreningTemplate()
                {
                    NazivPredloska = ""
                };
                predlozakModel.ITT = ITT;

                _context.IstezanjeTreningTemplate.Add(predlozakModel.ITT);
                _context.SaveChanges();
            }

            return View(predlozakModel);
        }

        [HttpPost]
        public ActionResult DodajPredlozakIstezanja(IstezanjeTemplateListaIstezanjaModel predlozakModel)
        {
            var query = from x in _context.IstezanjeTreningTemplate
                        where x.IstezanjeTreningTemplateId == predlozakModel.ITT.IstezanjeTreningTemplateId
                        select x;

            var ITT = query.Single();

            ITT.NazivPredloska = predlozakModel.ITT.NazivPredloska;

            _context.Entry(ITT).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("PredlosciIstezanjePopis");
        }

        [HttpGet]
        public ActionResult DodajIstezanjeUPredlozak(int id = 0, int predlozakIzmijeni = 0)
        {
            var listaIstezanja = _context.IstezanjePopis.ToList();
            ViewData["predlozakIzmijeni"] = predlozakIzmijeni;

            if (listaIstezanja.Count == 0)
            {
                listaIstezanja = new List<IstezanjePopis> { new IstezanjePopis { IstezanjeId = 0, Naziv = "", Info = "" } };
                ViewData["TemplateId"] = id;
                return View(listaIstezanja);
            }

            ViewData["TemplateId"] = id;
            return View(listaIstezanja);
        }

        [HttpPost]
        public ActionResult DodajIstezanjeUPredlozak(int id1 = 0, int id2 = 0, int predlozakIzmijeni = 0)
        {
            var queryIst  = from x in _context.IstezanjePopis
                            where x.IstezanjeId == id1
                            select x;

            var queryTemp = from t in _context.IstezanjeTreningTemplate
                            where t.IstezanjeTreningTemplateId == id2
                            select t;

            var ist = queryIst.Single();

            var temp = queryTemp.Single();

            IstezanjeT istT = new IstezanjeT() {
            NazivT = ist.Naziv,
            InfoT = ist.Info,
            IstezanjeTreningTemplateIstezanjeTreningTemplateId = id2,
            IstezanjePopisIstezanjeId = ist.IstezanjeId
            };

            _context.IstezanjeT.Add(istT);
            _context.SaveChanges();

            return predlozakIzmijeni != 0
                ? RedirectToAction("IzmijeniPredlozakIstezanja", new { id = id2 })
                : RedirectToAction("DodajPredlozakIstezanja", new { id = id2 });
        }

        public ActionResult IzbrisiIstezanjeIzPredloska(int id = 0, int id1 = 0, int predlozakIzmijeni = 0)
        {
            var istT = _context.IstezanjeT.Find(id);

            _context.IstezanjeT.Remove(istT);
            _context.SaveChanges();

            return predlozakIzmijeni != 0
                ? RedirectToAction("IzmijeniPredlozakIstezanja", new { id = id1 })
                : RedirectToAction("DodajPredlozakIstezanja", new { id = id1 });
        }

        #endregion IstezanjePredlozak

        #region Clan
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

                var age = (short)((DateTime.Now - c.GodinaRodenja).TotalDays / 365.242199);
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
                                 join z in _context.ZagrijavanjeSkupina on x.TreningId equals z.TreningTreningId
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
                        foreach(Zagrijavanje z in b.Zagrijavanje.ToList())
                        {
                            _context.Zagrijavanje.Remove(z);
                        }
                        foreach (VjezbaZagrijavanje vz in b.VjezbaZagrijavanje.ToList())
                        {
                            _context.VjezbaZagrijavanjeSet.Remove(vz);
                        }
                        _context.ZagrijavanjeSkupina.Remove(b);
                    }

                    foreach (var b in query2.ToList())
                    {
                        foreach (Vjezba vj in b.Vjezba.ToList())
                        {
                            _context.Vjezba.Remove(vj);
                        }
                        foreach (AerobneVjezbe avj in b.AerobneVjezbe.ToList())
                        {
                            _context.AerobneVjezbe.Remove(avj);
                        }
                        foreach (AnaerobneVjezbe nvj in b.AnaerobneVjezbe.ToList())
                        {
                            _context.AnaerobneVjezbe.Remove(nvj);
                        }
                        foreach (ZagrijavanjeVjezba zvj in b.ZagrijavanjeVjezba.ToList())
                        {
                            _context.ZagrijavanjeVjezbaSet.Remove(zvj);
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

            var queryslikeclana = from x in _context.ClanSlike
                                  join cl in _context.Clan on x.ClanSlikeId equals cl.ClanId
                                  where x.ClanClanId == id
                                  select x;
            if (queryslikeclana.FirstOrDefault() != null)
            {
                foreach (var slik in queryslikeclana)
                {
                    _context.ClanSlike.Remove(slik);
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
                var age = (short)((DateTime.Now - c.GodinaRodenja).TotalDays / 365.242199);
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

            var queryAmneza = from x in _context.Amneza
                                      where x.ClanClanId == id
                                      select x;

            var ctm = new ClanTestModel { Clan = c, ListaTest = queryTest.ToList(), ListAntropometrija = queryAntropometrija.ToList(), ListaAmneza = queryAmneza.ToList() };

            return View(ctm);
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
        #endregion Clan

        #region Test
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

            var listaT = new TestList { Clan = c };

            if (query.FirstOrDefault() != null)
            {
                listaT.ListaTestova = query.ToList();
            }

            return View(listaT);
        }

        [HttpGet]
        public ActionResult DodajTest(int id = 0)
        {
            var t = new Test { ClanId = id };
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

            return RedirectToAction("Test", new { id = t.ClanId });
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

                return RedirectToAction("Test", new { id = t.ClanId });
            }
            return RedirectToAction("IzmijeniTest", t);
        }

        public ActionResult DetaljiTest(int id)
        {
            var t = _context.Test.Find(id);
            return View(t);

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

        [HttpGet]
        public ActionResult DodajFunkcionalniRezultat(int id = 0)
        {
            FunkcionalniRezultatiTest ft = new FunkcionalniRezultatiTest() { TestId = id };
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
        #endregion Test

        #region Amneza
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
        #endregion Amneza

        #region Antropometrija
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
        #endregion Antropometrija

        #region Trening
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
                    var query6 = from x in _context.Trening
                                 join z in _context.ZagrijavanjeSkupina on x.TreningId equals z.TreningTreningId
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

                    foreach (SekcijaVjezbi a in query2.ToList())
                    {
                        foreach (Vjezba vj in a.Vjezba.ToList())
                        {
                            _context.Vjezba.Remove(vj);
                        }
                        foreach (AerobneVjezbe avj in a.AerobneVjezbe.ToList())
                        {
                            _context.AerobneVjezbe.Remove(avj);
                        }
                        foreach (AnaerobneVjezbe nvj in a.AnaerobneVjezbe.ToList())
                        {
                            _context.AnaerobneVjezbe.Remove(nvj);
                        }
                        foreach (ZagrijavanjeVjezba zvj in a.ZagrijavanjeVjezba.ToList())
                        {
                            _context.ZagrijavanjeVjezbaSet.Remove(zvj);
                        }
                        _context.SekcijaVjezbi.Remove(a);
                    }

                    foreach (ZagrijavanjeSkupina zs in query6.ToList())
                    {
                        foreach(Zagrijavanje z in zs.Zagrijavanje.ToList())
                        {
                            _context.Zagrijavanje.Remove(z);
                        }
                        foreach (VjezbaZagrijavanje vz in zs.VjezbaZagrijavanje.ToList())
                        {
                            _context.VjezbaZagrijavanjeSet.Remove(vz);
                        }
                        _context.ZagrijavanjeSkupina.Remove(zs);
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
                        select new { x, y };

            var query1 = from x in _context.Trening
                         join z in _context.ZagrijavanjeSkupina on x.TreningId equals z.TreningTreningId
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
                    foreach(Zagrijavanje z in a.Zagrijavanje.ToList())
                    {
                        _context.Zagrijavanje.Remove(z);
                    }
                    foreach(VjezbaZagrijavanje vz in a.VjezbaZagrijavanje.ToList())
                    {
                        _context.VjezbaZagrijavanjeSet.Remove(vz);
                    }
                    _context.ZagrijavanjeSkupina.Remove(a);
                }

                foreach (var a in query2.ToList())
                {
                    foreach (Vjezba vj in a.Vjezba.ToList())
                    {
                        _context.Vjezba.Remove(vj);
                    }
                    foreach (AerobneVjezbe avj in a.AerobneVjezbe.ToList())
                    {
                        _context.AerobneVjezbe.Remove(avj);
                    }
                    foreach (AnaerobneVjezbe nvj in a.AnaerobneVjezbe.ToList())
                    {
                        _context.AnaerobneVjezbe.Remove(nvj);
                    }
                    foreach (ZagrijavanjeVjezba zvj in a.ZagrijavanjeVjezba.ToList())
                    {
                        _context.ZagrijavanjeVjezbaSet.Remove(zvj);
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

                return RedirectToAction("TreningPopis", new { id = k });
            }
            return null;
        }

        [HttpGet]
        public ActionResult IzmijeniTrening(int id = 0)
        {
            var query = from x in _context.Trening
                        join y in _context.Clan on x.ClanId equals y.ClanId
                        where x.TreningId == id
                        select new { x, y };

            var query1 = from x in _context.Trening
                         join z in _context.ZagrijavanjeSkupina on x.TreningId equals z.TreningTreningId
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
                        TreningDatum = (DateTime)firstOrDefault.x.DatumTreninga,
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
                        trm.ZagrijavanjeSkupina = query1.ToList();
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

            return RedirectToAction("TreningPopis", new { id = tr.ClanId });
        }

        [HttpGet]
        public ActionResult DetaljiTrening(int id = 0)
        {
            var query = from x in _context.Trening
                        join y in _context.Clan on x.ClanId equals y.ClanId
                        where x.TreningId == id
                        select new { x, y };

            var query1 = from x in _context.Trening
                         join z in _context.ZagrijavanjeSkupina on x.TreningId equals z.TreningTreningId
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
                        TreningDatum = (DateTime)firstOrDefault.x.DatumTreninga,
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
                        trm.ZagrijavanjeSkupina = query1.ToList();
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

                var query5 = from x in _context.ZagrijavanjeSkupina
                             join y in _context.Trening on x.TreningTreningId equals y.TreningId
                             where y.TreningId == id
                             select x;

                trm.ZagrijavanjeSkupina = query5.ToList();

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

                var query5 = from x in _context.ZagrijavanjeSkupina
                             join y in _context.Trening on x.TreningTreningId equals y.TreningId
                             where y.TreningId == id
                             select x;

                trm.ZagrijavanjeSkupina = query5.ToList();

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

            return RedirectToAction("TreningPopis", new { id = tr.ClanId });
        }
        #endregion Trening

        #region ZagrijavanjeTrening
        [HttpGet]
        public ActionResult DodajZagrijavanjeTrening(int id, int sekcija, int izmijeni = 0, int counter = 0, string refferer = "")
        {
            var zgp = new IstezanjeZagrijavanjeLista
            {
                ZagrijavanjePopis = _context.ZagrijavanjePopis.ToList(),
                TreningId = id,
                Izmijeni = izmijeni
            };
            ViewData["izmijeni"] = izmijeni;
            ViewData["counter"] = counter;
            ViewData["refferer"] = refferer;
            ViewData["sekcija"] = sekcija;
            return View(zgp);
        }

        [HttpPost]
        public ActionResult DodajZagrijavanjeTrening(int zgpId = 0, int id = 0, int zgpIzmijeni = 0, int counter = 0, int skupina = 0)
        {
            var query = from x in _context.ZagrijavanjePopis
                        where x.ZagrijavanjeId == zgpId
                        select x;

            var zgp = query.Single();

            if (skupina == 0)
            { 
                var skupina1 = new ZagrijavanjeSkupina { TreningTreningId = id };
                _context.ZagrijavanjeSkupina.Add(skupina1);
                _context.SaveChanges();
            }

            var query1 = from x in _context.ZagrijavanjeSkupina
                         where x.TreningTreningId == id
                         select x.ZagrijavanjeSkupinaId;

            var sId = Convert.ToInt32(query1.FirstOrDefault());

            var zg = new Zagrijavanje { Naziv = zgp.Naziv, ZagrijavanjePopisZagrijavanjeId = zgpId, ZagrijavanjeSkupinaZagrijavanjeSkupinaId  = sId };
            if (zgp.Info != null)
            {
                zg.Info = zgp.Info;
            }

            _context.Zagrijavanje.Add(zg);
            _context.SaveChanges();

            var idVjezba = zg.ZagrijavanjeId + "ZZ,";
            var sekcija = _context.ZagrijavanjeSkupina.Find(sId);

            if (sekcija.PopisZagrijavanja != null)
            {
                sekcija.PopisZagrijavanja = sekcija.PopisZagrijavanja.ToString() + idVjezba.ToString();
            }
            else
            {
                sekcija.PopisZagrijavanja = idVjezba.ToString();
            }

            _context.Entry(sekcija).State = EntityState.Modified;
            _context.SaveChanges();
            
            ViewData["izmijeni"] = zgpIzmijeni;
            ViewData["counter"] = counter;
            ViewData["skupina"] = skupina;
            return zgpIzmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpGet]
        public ActionResult IzbrisiZagrijavanjeTrening(int id = 0, int izmijeni = 0, int index = 0)
        {
            var zg = _context.Zagrijavanje.Find(id);

            var sku = _context.ZagrijavanjeSkupina.Find(zg.ZagrijavanjeSkupinaZagrijavanjeSkupinaId);
            id = sku.TreningTreningId;
            var zgIds = sku.PopisZagrijavanja.ToString();
            List<string> ZagrijavanjaList = new List<string>();

            foreach (string zagrijavanjeId in zgIds.Split(','))
            {
                ZagrijavanjaList.Add(zagrijavanjeId);
            }
            ZagrijavanjaList.RemoveAt(index);
            string novaLista = string.Join(",", ZagrijavanjaList.ToArray());
            sku.PopisZagrijavanja = novaLista;
            _context.Entry(sku).State = EntityState.Modified;
            _context.SaveChanges();
            _context.Zagrijavanje.Remove(zg);
            _context.SaveChanges();

            return izmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
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
        #endregion ZagrijavanjeTrening

        #region VjezbeTrening
        [HttpGet]
        public ActionResult DodajVjezbuTrening(int id, string id1, int izmijeni = 0, int counter = 0, int skupina = 0, string refferer = "")
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
            ViewData["skupina"] = skupina;
            ViewData["refferer"] = refferer;
            return View(vj);
        }

        [HttpPost]
        public ActionResult DodajVjezbuTrening(int id = 0, int vjpId = 0, int id1 = 0, int vjpIzmijeni = 0, int counter = 0)
        {
            var query = from x in _context.VjezbePopis
                        where x.VjezbeId == vjpId
                        select x;

            var vjp = query.Single();

            
            var vj = new Vjezba { ImeVjezbe = vjp.ImeVjezbe, SekcijaId = id, VjezbePopisVjezbeId = vjpId };
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

            var idVjezba = vj.VjezbaId + "VV,";
            var sekcija = _context.SekcijaVjezbi.Find(id);

            if (sekcija.PopisVjezbi != null)
            {
                sekcija.PopisVjezbi = sekcija.PopisVjezbi.ToString() + idVjezba.ToString();
            }
            else
            {
                sekcija.PopisVjezbi = idVjezba.ToString();
            }
            _context.Entry(sekcija).State = EntityState.Modified;
            _context.SaveChanges();

            var trening = _context.Trening.Find(sekcija.TreningId);
            id = trening.TreningId;
  
            ViewData["izmijeni"] = vjpIzmijeni;
            ViewData["counter"] = counter;
            ViewData["id"] = "sek";            

            return vjpIzmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpPost]
        public ActionResult DodajVjezbuUZagrijavanjeTrening(int id = 0, int vjpId = 0, int id1 = 0, int vjpIzmijeni = 0, int counter = 0, int skupina = 0, string refferer = "")
        {
            var query = from x in _context.VjezbePopis
                        where x.VjezbeId == vjpId
                        select x;

            var vjp = query.Single();

            if(skupina == 0 && refferer == "zagrijavanje")
            {
                var zskupina = new ZagrijavanjeSkupina { TreningTreningId = id1 };
                _context.ZagrijavanjeSkupina.Add(zskupina);
                _context.SaveChanges();   
            }

            var query1 = from x in _context.ZagrijavanjeSkupina
                        where x.TreningTreningId == id1
                        select x.ZagrijavanjeSkupinaId;

            var sId = Convert.ToInt32(query1.FirstOrDefault());

            var vj = new VjezbaZagrijavanje { ImeVjezbe = vjp.ImeVjezbe, ZagrijavanjeSkupinaZagrijavanjeSkupinaId = sId, VjezbePopisVjezbeId = vjpId };

            if (vjp.Info != null)
            {
                vj.Info = vjp.Info;
            }
            if (vjp.Slika != null)
            {
                vj.Slika = vjp.Slika;
            }

            _context.VjezbaZagrijavanjeSet.Add(vj);
            _context.SaveChanges();

            var idVjezba = vj.VjezbaId + "VZ,";
            var skupina1 = _context.ZagrijavanjeSkupina.Find(sId);

            if (skupina1.PopisZagrijavanja != null)
            {
                skupina1.PopisZagrijavanja = skupina1.PopisZagrijavanja.ToString() + idVjezba.ToString();
            }
            else
            {
                skupina1.PopisZagrijavanja = idVjezba.ToString();
            }
            _context.Entry(skupina1).State = EntityState.Modified;
            _context.SaveChanges();

            var trening = _context.Trening.Find(skupina1.TreningTreningId);
            id = trening.TreningId;

            ViewData["izmijeni"] = vjpIzmijeni;
            ViewData["counter"] = counter;
            ViewData["skupina"] = skupina;
            ViewData["id"] = "sek";

            return vjpIzmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpGet]
        public ActionResult DodajAerobnuVjezbuTrening(int id, string id1, int izmijeni = 0, int counter = 0)
        {
            var vj = new AerobneVjezbePopisLista
            {
                AerobneVjezbePopis = _context.AerobneVjezbePopis.ToList(),
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
        public ActionResult DodajAerobnuVjezbuTrening(int avjpId = 0, int id = 0, int avjpIzmijeni = 0, int counter = 0)
        {
            var query = from x in _context.AerobneVjezbePopis
                        where x.AerobnaVjezbaId == avjpId
                        select x;

            var avjp = query.Single();

            var avj = new AerobneVjezbe { Naziv = avjp.Naziv, SekcijaVjezbiSekcijaId = id, AerobneVjezbePopisAerobnaVjezbaId = avjpId };
            if (avjp.Info != null)
            {
                avj.Info = avjp.Info;
            }
            if (avjp.Slika != null)
            {
                avj.Slika = avjp.Slika;
            }
            
            _context.AerobneVjezbe.Add(avj);
            _context.SaveChanges();
            var idVjezba = avj.AerobnaVjezbaId + "AV,";
            var sekcija = _context.SekcijaVjezbi.Find(id);

            if (sekcija.PopisVjezbi != null)
            {
                sekcija.PopisVjezbi = sekcija.PopisVjezbi.ToString() + idVjezba.ToString();
            }
            else
            {
                sekcija.PopisVjezbi = idVjezba.ToString();
            }
            _context.Entry(sekcija).State = EntityState.Modified;
            _context.SaveChanges();

            var trening = _context.Trening.Find(sekcija.TreningId);
            id = trening.TreningId;

            ViewData["izmijeni"] = avjpIzmijeni;
            ViewData["counter"] = counter;
            ViewData["id"] = "sek";
            return avjpIzmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpPost]
        public ActionResult DodajZagrijavanjeUVjezbuTrening(int zgpId = 0, int id = 0, int zgpIzmijeni = 0, int sekcija = 0, int counter = 0)
        {         
            var query = from x in _context.ZagrijavanjePopis
                        where x.ZagrijavanjeId == zgpId
                        select x;

            var zvjp = query.Single();

            var zvj = new ZagrijavanjeVjezba { Naziv = zvjp.Naziv, SekcijaVjezbiSekcijaId = sekcija, ZagrijavanjePopisZagrijavanjeId1 = zgpId }; //TreningId = id, ZagrijavanjeId = zvjp.ZagrijavanjeId, 
            if (zvjp.Info != null)
            {
                zvj.Info = zvjp.Info;
            }
            if (zvjp.Slika != null)
            {
                zvj.Slika = zvjp.Slika;
            }
            _context.ZagrijavanjeVjezbaSet.Add(zvj);
            _context.SaveChanges();
            var idVjezba = zvj.ZagrijavanjeVjezbaId + "ZV,";
            var sekcija1 = _context.SekcijaVjezbi.Find(sekcija);

            if (sekcija1.PopisVjezbi != null)
            {
                sekcija1.PopisVjezbi = sekcija1.PopisVjezbi.ToString() + idVjezba.ToString();
            }
            else
            {
                sekcija1.PopisVjezbi = idVjezba.ToString();
            }
            _context.Entry(sekcija1).State = EntityState.Modified;
            _context.SaveChanges();

            var trening = _context.Trening.Find(sekcija1.TreningId);
            id = trening.TreningId;

            ViewData["izmijeni"] = zgpIzmijeni;
            ViewData["counter"] = counter;
            ViewData["sekcija"] = sekcija;
            ViewData["id"] = "sek";
            return zgpIzmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpGet]
        public ActionResult DodajAnaerobnuVjezbuTrening(int id, string id1, int izmijeni = 0, int counter = 0)
        {
            var vj = new AnaerobneVjezbePopisLista
            {
                AnaerobneVjezbePopis = _context.AnaerobneVjezbePopis.ToList(),
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
        public ActionResult DodajAnaerobnuVjezbuTrening(int avjpId = 0, int id = 0, int avjpIzmijeni = 0, int counter = 0)
        {
            var query = from x in _context.AnaerobneVjezbePopis
                        where x.AnaerobnaVjezbaId == avjpId
                        select x;

            var avjp = query.Single();

            var avj = new AnaerobneVjezbe { Naziv = avjp.Naziv, SekcijaVjezbiSekcijaId = id, AnaerobneVjezbePopisAnaerobnaVjezbaId = avjpId };
            if (avjp.Info != null)
            {
                avj.Info = avjp.Info;
            }
            if (avjp.Slika != null)
            {
                avj.Slika = avjp.Slika;
            }
            
            _context.AnaerobneVjezbe.Add(avj);
            _context.SaveChanges();
            var idVjezba = avj.AnaerobnaVjezbaId + "NV,";
            var sekcija = _context.SekcijaVjezbi.Find(id);

            if (sekcija.PopisVjezbi != null)
            {
                sekcija.PopisVjezbi = sekcija.PopisVjezbi.ToString() + idVjezba.ToString();
            }
            else
            {
                sekcija.PopisVjezbi = idVjezba.ToString();
            }
            _context.Entry(sekcija).State = EntityState.Modified;
            _context.SaveChanges();

            var trening = _context.Trening.Find(sekcija.TreningId);
            id = trening.TreningId;

            ViewData["izmijeni"] = avjpIzmijeni;
            ViewData["counter"] = counter;
            ViewData["id"] = "sek";
            return avjpIzmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpGet]
        public ActionResult IzbrisiVjezbuTrening(int id = 0, int index = 0, int izmijeni = 0)
        {
            var vj = _context.Vjezba.Find(id);
            var sec = _context.SekcijaVjezbi.Find(vj.SekcijaId);
            id = sec.TreningId;

            var vjezbeIds = sec.PopisVjezbi.ToString();
            List<string> vjezbeList = new List<string>();
            foreach (string vjezbaId in vjezbeIds.Split(','))
            {
                vjezbeList.Add(vjezbaId);
            }
            vjezbeList.RemoveAt(index);
            string novaListaVjezbi = string.Join(",", vjezbeList.ToArray());
            sec.PopisVjezbi = novaListaVjezbi;
            _context.Entry(sec).State = EntityState.Modified;
            _context.SaveChanges();

            _context.Vjezba.Remove(vj);
            _context.SaveChanges();

            return izmijeni != 0
            ? RedirectToAction("IzmijeniTrening", new { id })
            : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpGet]
        public ActionResult IzbrisiVjezbuIzZagrijavanjaTrening(int id = 0, int index = 0, int izmijeni = 0)
        {
            var vjz = _context.VjezbaZagrijavanjeSet.Find(id);
            var sku = _context.ZagrijavanjeSkupina.Find(vjz.ZagrijavanjeSkupinaZagrijavanjeSkupinaId);
            id = sku.TreningTreningId;

            var vjzIds = sku.PopisZagrijavanja.ToString();
            List<string> vjezbeZagrijavanjaList = new List<string>();
            foreach (string vjezbaZagrijavanjeId in vjzIds.Split(','))
            {
                vjezbeZagrijavanjaList.Add(vjezbaZagrijavanjeId);
            }
            vjezbeZagrijavanjaList.RemoveAt(index);

            string novaLista = string.Join(",", vjezbeZagrijavanjaList.ToArray());
            sku.PopisZagrijavanja = novaLista;
            _context.Entry(sku).State = EntityState.Modified;
            _context.SaveChanges();
            _context.VjezbaZagrijavanjeSet.Remove(vjz);
            _context.SaveChanges();

            return izmijeni != 0
            ? RedirectToAction("IzmijeniTrening", new { id })
            : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpGet]
        public ActionResult IzbrisiAerobnuVjezbuTrening(int id = 0, int index = 0, int izmijeni = 0)
        {
            var vj = _context.AerobneVjezbe.Find(id);
            var sec = _context.SekcijaVjezbi.Find(vj.SekcijaVjezbiSekcijaId);
            id = sec.TreningId;

            var vjezbeIds = sec.PopisVjezbi.ToString();
            List<string> vjezbeList = new List<string>();
            foreach (string vjezbaId in vjezbeIds.Split(','))
            {
                vjezbeList.Add(vjezbaId);
            }
            vjezbeList.RemoveAt(index);
            string novaListaVjezbi = string.Join(",", vjezbeList.ToArray());
            sec.PopisVjezbi = novaListaVjezbi;
            _context.Entry(sec).State = EntityState.Modified;
            _context.SaveChanges();

            _context.AerobneVjezbe.Remove(vj);
            _context.SaveChanges();

            return izmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpGet]
        public ActionResult IzbrisiAnaerobnuVjezbuTrening(int id = 0, int index = 0, int izmijeni = 0)
        {
            var vj = _context.AnaerobneVjezbe.Find(id);
            var sec = _context.SekcijaVjezbi.Find(vj.SekcijaVjezbiSekcijaId);
            id = sec.TreningId;

            var vjezbeIds = sec.PopisVjezbi.ToString();
            List<string> vjezbeList = new List<string>();
            foreach (string vjezbaId in vjezbeIds.Split(','))
            {
                vjezbeList.Add(vjezbaId);
            }
            vjezbeList.RemoveAt(index);
            string novaListaVjezbi = string.Join(",", vjezbeList.ToArray());
            sec.PopisVjezbi = novaListaVjezbi;
            _context.Entry(sec).State = EntityState.Modified;
            _context.SaveChanges();

            _context.AnaerobneVjezbe.Remove(vj);
            _context.SaveChanges();

            return izmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpGet]
        public ActionResult IzbrisiZagrijavanjeIzVjezbeTrening(int id = 0, int index = 0, int izmijeni = 0)
        {
            var vj = _context.ZagrijavanjeVjezbaSet.Find(id);
            var sec = _context.SekcijaVjezbi.Find(vj.SekcijaVjezbiSekcijaId);
            id = sec.TreningId;

            var vjezbeIds = sec.PopisVjezbi.ToString();
            List<string> vjezbeList = new List<string>();
            foreach (string vjezbaId in vjezbeIds.Split(','))
            {
                vjezbeList.Add(vjezbaId);
            }
            vjezbeList.RemoveAt(index);
            string novaListaVjezbi = string.Join(",", vjezbeList.ToArray());
            sec.PopisVjezbi = novaListaVjezbi;
            _context.Entry(sec).State = EntityState.Modified;
            _context.SaveChanges();

            _context.ZagrijavanjeVjezbaSet.Remove(vj);
            _context.SaveChanges();

            return izmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpPost]
        public ActionResult SpremiVjezbuInfo(string brojPonavljanja, string brojSerija = null, string tezina = null,
            string odmor = null, int id = 0, int vjezbaId = 0, int izmijeni = 0, string refferer = "")
        {
            if (refferer == "zagrijavanje")
            { 
                var query1 = from x in _context.VjezbaZagrijavanjeSet
                             where x.VjezbaId == vjezbaId
                             select x;
                var vj = query1.Single();

                vj.BrojPonavljanja = brojPonavljanja;
                vj.BrojSerija = brojSerija;
                vj.Kilogrami = tezina;
                vj.Odmor = odmor;

                _context.Entry(vj).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
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
            }     
            
            return izmijeni != 0
               ? RedirectToAction("IzmijeniTrening", new { id })
               : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpPost]
        public ActionResult SpremiAerobnuVjezbuInfo(string Puls, string Tempo = null, string Trajanje = null, string Odmor = null,
            string ANapomena = null, int id = 0, int vjezbaId = 0, int izmijeni = 0)
        {
            var query = from x in _context.AerobneVjezbe
                        where x.AerobnaVjezbaId == vjezbaId
                        select x;

            var vj = query.Single();
            vj.Puls = Puls;
            vj.Tempo = Tempo;
            vj.Trajanje = Trajanje;
            vj.Odmor = Odmor;
            vj.Napomena = ANapomena;

            _context.Entry(vj).State = EntityState.Modified;
            _context.SaveChanges();

            return izmijeni != 0
               ? RedirectToAction("IzmijeniTrening", new { id })
               : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        public ActionResult SpremiAnaerobnuVjezbuInfo(string Puls = null, string Tempo = null, string BrojSprintova = null,
            string TrajanjeSprinta = null, string Odmor = null, string NNapomena = null, int id = 0, int vjezbaId = 0, int izmijeni = 0)
        {
            var query = from x in _context.AnaerobneVjezbe
                        where x.AnaerobnaVjezbaId == vjezbaId
                        select x;

            var vj = query.Single();
            vj.Puls = Puls;
            vj.Tempo = Tempo;
            vj.BrojSprintova = BrojSprintova;
            vj.TrajanjeSprinta = TrajanjeSprinta;
            vj.Odmor = Odmor;
            vj.Napomena = NNapomena;

            _context.Entry(vj).State = EntityState.Modified;
            _context.SaveChanges();

            return izmijeni != 0
               ? RedirectToAction("IzmijeniTrening", new { id })
               : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        public ActionResult SpremiZagrijavanjeUVjezbuInfo(string Puls = null, string Tempo = null,
            string Trajanje = null, string Odmor = null, string ZagrijavanjeNapomena = null, int id = 0, int vjezbaId = 0, int izmijeni = 0)
        {
            var query = from x in _context.ZagrijavanjeVjezbaSet
                        where x.ZagrijavanjeVjezbaId == vjezbaId
                        select x;

            var vj = query.Single();
            vj.Puls = Puls;
            vj.Tempo = Tempo;
            vj.Trajanje = Trajanje;
            vj.Odmor = Odmor;
            vj.ZagrijavanjeNapomena = ZagrijavanjeNapomena;

            _context.Entry(vj).State = EntityState.Modified;
            _context.SaveChanges();

            return izmijeni != 0
               ? RedirectToAction("IzmijeniTrening", new { id })
               : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }
        #endregion VjezbeTrening
        
        #region IstezanjeTrening
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

            var ist = new Istezanje { Naziv = istp.Naziv, TreningId = id, IstezanjePopisIstezanjeId = istpId };
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
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        [HttpGet]
        public ActionResult IzbrisiIstezanjeTrening(int id = 0, int izmijeni = 0)
        {
            var ist = _context.Istezanje.Find(id);
            id = ist.TreningId;
            _context.Istezanje.Remove(ist);
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
        #endregion IstezanjeTrening

        #region PredlozakTrening

        [HttpGet]
        public ActionResult DodajPredlozakTrening(int id, int izmijeni = 0)
        {
            var pp = _context.IstezanjeTreningTemplate.ToList();
            ViewData["treningId"] = id;
            ViewData["izmijeni"] = izmijeni;
            return View(pp);
        }

        [HttpPost]
        public ActionResult DodajPredlozakTrening(int id = 0, int templateId = 0, int izmijeni = 0)
        {
            var trening = _context.Trening.Find(id);

            var predlozak = _context.IstezanjeTreningTemplate.Find(templateId);

            var query = from x in _context.IstezanjeT
                        where x.IstezanjeTreningTemplateIstezanjeTreningTemplateId == predlozak.IstezanjeTreningTemplateId
                        select x;

            Istezanje i = new Istezanje();

            foreach (IstezanjeT it in query.ToList())
            {
                i.Naziv = it.NazivT;
                i.Info = it.InfoT;
                i.TreningId = id;
                i.IstezanjePopisIstezanjeId = it.IstezanjePopisIstezanjeId;
                _context.Istezanje.Add(i);
                _context.SaveChanges();
            }

            ViewData["izmijeni"] = izmijeni;

            return izmijeni != 0
                ? RedirectToAction("IzmijeniTrening", new { id })
                : RedirectToAction("DodajTrening", new { id, DodajVjezbu = 2 });
        }

        #endregion PredlozakTrening

        #region SekcijeTrening
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
            if (sekcija.AerobneVjezbe != null)
            {
                foreach (var av in sekcija.AerobneVjezbe.ToList())
                {
                    _context.AerobneVjezbe.Remove(av);
                }
            }

            if (sekcija.AnaerobneVjezbe != null)
            {
                foreach (var nv in sekcija.AnaerobneVjezbe.ToList())
                {
                    _context.AnaerobneVjezbe.Remove(nv);
                }
            }

            if (sekcija.ZagrijavanjeVjezba != null)
            {
                foreach (var zv in sekcija.ZagrijavanjeVjezba.ToList())
                {
                    _context.ZagrijavanjeVjezbaSet.Remove(zv);
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
        #endregion SekcijeTrening      
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