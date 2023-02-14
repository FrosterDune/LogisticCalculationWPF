﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCalculationWPF.Model
{
    public class AnalyzaZasobModel
    {
        private double? Spotreba { get; set; }
        private double? ObjednavaciDavka { get; set; }
        private double? PojistnaZasoba { get; set; }
        private double? PokrytiPoptavky { get; set; }
        private double? DodaciLhuta { get; set; }
        public double? DnyNaTyden { get; set; }
        private int? IntervalKontroly { get; set; }
        private int Systemy { get; set; }
        private double? xPojistnaZasoba { get; set; }
        private double? OcekavanaSpotreba { get; set; }

        public AnalyzaZasobModel(double? spotreba, double? objednavaciDavka, double? pojistnaZasoba, double? pokrytiPoptavky, double? dodaciLhuta, double? dnynarok,
            int? intervalKontroly, int systemy)
        {
            Spotreba = spotreba;
            ObjednavaciDavka = objednavaciDavka;
            PojistnaZasoba = pojistnaZasoba;
            PokrytiPoptavky = pokrytiPoptavky;
            DodaciLhuta = dodaciLhuta;
            DnyNaTyden = dnynarok;
            IntervalKontroly = intervalKontroly;
            Systemy = systemy;
            OcekavanaSpotreba = Spotreba / DnyNaTyden;
            xPojistnaZasoba = OcekavanaSpotreba * PokrytiPoptavky;
        }
        
        private double BQsystem()
        {
            switch (PokrytiPoptavky)
            {
                case > 0:
                    return Math.Ceiling(Convert.ToDouble(xPojistnaZasoba + DodaciLhuta * OcekavanaSpotreba));
                default:
                    return Math.Ceiling(Convert.ToDouble(PojistnaZasoba + DodaciLhuta * OcekavanaSpotreba));                    
            }
        }

        private double SQsystem()
        {
            switch (PokrytiPoptavky)
            {
                case > 0:
                    return Math.Ceiling(Convert.ToDouble(xPojistnaZasoba + OcekavanaSpotreba * (DodaciLhuta + 0.7 * IntervalKontroly)));                     
                default:
                    return Math.Ceiling(Convert.ToDouble(PojistnaZasoba + OcekavanaSpotreba * (DodaciLhuta + 0.7 * IntervalKontroly)));
            }
        }
        
        public double ObjednavaciUrovenVysledek()
        {
            return Systemy switch
            {
                0 => BQsystem(),
                1 => SQsystem(),
                _ => 0
            }; 
            
        }        
        public double PrumernaZasoba()
        {
            double? TydnyNaDny = DnyNaTyden * 7;
            return Math.Round(Convert.ToDouble(TydnyNaDny / OcekavanaSpotreba), 2);
        }
        public double PocetObjednavekZaRok()
        {
            return Math.Ceiling(Convert.ToDouble(Spotreba / ObjednavaciDavka));
        }
    }
}