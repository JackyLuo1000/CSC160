using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadingOperators
{ 
    public struct Fraction
    {
        private int denominator;

        public int WholeNumber { get; set; }
        public int Numerator { get; set; }
        public int Denominator
        {
            get { return denominator; }
            set {
                    if(value != 0)
                    {
                        denominator = value;
                    }
                    else
                    {
                        throw new ArgumentException("The denominator cannot be 0.");
                    }
                }
        }


        public Fraction(int wholeNumber=0, int numerator=0, int denominator=1) : this()
        {
            this.denominator = 0;
            WholeNumber = 0;
            Numerator = 0;
            Denominator = 1;
            int negCount = 0;
            bool negDen = false;
            if (wholeNumber < 0)
            {
                negCount += 1;
            }
            if (numerator < 0)
            {
                negCount += 1;
            }
            if (denominator < 0)
            {
                negCount += 1;
                negDen = true;
            }
            if (negCount == 2)
            {
                wholeNumber = wholeNumber * -1;
                numerator = numerator * -1;
                denominator = denominator * -1;
            }
            else if (negCount % 2 == 1)
            {
                if (wholeNumber == 0)
                {
                    if (negDen)
                    {
                        denominator = denominator * -1;
                        numerator = numerator * -1;
                    }
                }
                else if (numerator == 0)
                {
                    if (negDen)
                    {
                        denominator = denominator * -1;
                        wholeNumber = wholeNumber * -1;
                    }
                }
                else
                {
                    denominator = denominator * -1;
                    numerator = numerator * -1;
                }
            }
            WholeNumber = wholeNumber;
            Numerator = numerator;
            Denominator = denominator;
        }

        public static int GCD(int m, int n)
        {
            if(m < 0)
            {
                m = m * -1;
            }
            if(n < 0)
            {
                n = n * -1;
            }
            if (m == 0 || n == 0)
                return 0;
            
            if (m == n)
                return m;
            
            if (m > n)
                return GCD(m - n, n);

            return GCD(m, n - m);
        }

        public static Fraction operator+ (Fraction a, Fraction b)
        {
            int wholeNum;
            int numer;
            int denom;
            if (a.Denominator == b.Denominator)
            {
                wholeNum = a.WholeNumber + b.WholeNumber;
                numer = a.Numerator + b.Numerator;
                denom = a.Denominator;
            }
            else
            {
                wholeNum = a.WholeNumber + b.WholeNumber;
                int tmpANum = a.Numerator * b.Denominator;
                int tmpBNum = b.Numerator * a.Denominator;
                denom = a.Denominator * b.Denominator;
                numer = tmpANum + tmpBNum;
            }
            Fraction c = new Fraction(wholeNum, numer, denom);
            c.Simplify();
            return c;
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            int wholeNum = 0;
            int numer;
            int denom;
            if (a.Denominator == b.Denominator)
            {
                int tmpANum = (a.WholeNumber * a.Denominator) + a.Numerator;
                int tmpBNum = (b.WholeNumber * b.Denominator) + b.Numerator;
                numer = tmpANum - tmpBNum;
                denom = a.Denominator;
            }
            else
            {
                int tmpANum = (a.WholeNumber * a.Denominator) + a.Numerator;
                int tmpBnum = (b.WholeNumber * b.Denominator) + b.Numerator;
                tmpANum = tmpANum * b.Denominator;
                tmpBnum = tmpBnum * a.Denominator;
                numer = tmpANum - tmpBnum;
                denom = a.Denominator * b.Denominator;
            }
            Fraction c = new Fraction(wholeNum, numer, denom);
            c.Simplify();
            return c;
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            int wholeNum = 0;
            int numer;
            int denom;
            int tmpANum = (a.WholeNumber * a.Denominator) + a.Numerator;
            int tmpBNum = (b.WholeNumber * b.Denominator) + b.Numerator;
            numer = tmpANum * b.Denominator;
            denom = a.Denominator * tmpBNum;
            Fraction c = new Fraction(wholeNum, numer, denom);
            c.Simplify();
            return c;
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            int wholeNum = 0;
            int numer;
            int denom;
            int tmpANum = (a.WholeNumber * a.Denominator) + a.Numerator;
            int tmpBNum = (b.WholeNumber * b.Denominator) + b.Numerator;
            numer = tmpANum * tmpBNum;
            denom = a.Denominator * b.Denominator;
            Fraction c = new Fraction(wholeNum, numer, denom);
            c.Simplify();
            return c;
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            bool isEqual = false;
            a.MakeImproper();
            b.MakeImproper();
            a.Reduce();
            b.Reduce();
            if(a.Denominator == b.Denominator && a.Numerator == b.Numerator)
            {
                isEqual = true;
            }
            return isEqual;
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            bool isNotEqual = false;
            a.MakeImproper();
            b.MakeImproper();
            a.Reduce();
            b.Reduce();
            if (a.Denominator != b.Denominator || a.Numerator != b.Numerator)
            {
                isNotEqual = true;
            }
            return isNotEqual;
        }

        public void Reduce()
        {
            int gcd = GCD(this.Numerator, this.Denominator);
            if (gcd != 0)
            {
                this.Numerator = this.Numerator / gcd;
                this.Denominator = this.Denominator / gcd;
            }
        }

        public void MakeProper()
        {
            bool neg = false;
            if (this.Numerator < 0)
            {
                this.Numerator *= -1;
                neg = true;
            }
            while(this.Numerator >= this.Denominator)
            {
                this.Numerator = this.Numerator - this.Denominator;
                this.WholeNumber += 1;
            }
            if (neg && this.WholeNumber != 0)
            {
                this.WholeNumber *= -1;
            }else if(neg && this.WholeNumber == 0)
            {
                this.Numerator *= -1;
            }
        }

        public void MakeImproper()
        {
            this.Numerator = (this.WholeNumber * this.Denominator) + this.Numerator;
            this.WholeNumber = 0;
        }

        public void Simplify()
        {
            this.Reduce();
            this.MakeProper();
        }

        public override string ToString()
        {
            string result = "";
            if(this.WholeNumber != 0)
            {
                result += $"{this.WholeNumber} ";
            }
            if(this.Numerator != 0)
            {
                result += $"{this.Numerator}/{this.Denominator}";
            }
            if(this.WholeNumber == 0 && this.Numerator == 0)
            {
                result += $"{this.WholeNumber}";
            }
            return result.Trim();
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if(obj is Fraction){
                isEqual = this == (Fraction)obj;
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
