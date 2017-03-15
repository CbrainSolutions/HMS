using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityAllowanceDeduction
    /// </summary>
    public class EntityAllowanceDeduction
    {
        public EntityAllowanceDeduction()
        {
        }

        private int _AllowDedId;

        private string _Description;

        private int _Percentage;

        private decimal _Amount;

        private bool _IsPercentage;

        private bool _IsFixed;

        private bool _IsFlexible;

        private bool _IsAllowance;

        private bool _IsDeduction;

        public bool? IsBasic { get; set; }

        private bool _IsDelete;
        public string Type { get; set; }
        public string CategoryDesc { get; set; }
        public int SalDetail_Id { get; set; }
        public int AllowDedId
        {
            get
            {
                return this._AllowDedId;
            }
            set
            {
                if ((this._AllowDedId != value))
                {
                    this._AllowDedId = value;
                }
            }
        }

        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this._Description = value;
                }
            }
        }

        public int Percentage
        {
            get
            {
                return this._Percentage;
            }
            set
            {
                if ((this._Percentage != value))
                {
                    this._Percentage = value;
                }
            }
        }

        public decimal Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                if ((this._Amount != value))
                {
                    this._Amount = value;
                }
            }
        }

        public bool IsPercentage
        {
            get
            {
                return this._IsPercentage;
            }
            set
            {
                if ((this._IsPercentage != value))
                {
                    this._IsPercentage = value;
                }
            }
        }

        public bool IsFixed
        {
            get
            {
                return this._IsFixed;
            }
            set
            {
                if ((this._IsFixed != value))
                {
                    this._IsFixed = value;
                }
            }
        }

        public bool IsFlexible
        {
            get
            {
                return this._IsFlexible;
            }
            set
            {
                if ((this._IsFlexible != value))
                {
                    this._IsFlexible = value;
                }
            }
        }

        public bool IsAllowance
        {
            get
            {
                return this._IsAllowance;
            }
            set
            {
                if ((this._IsAllowance != value))
                {
                    this._IsAllowance = value;
                }
            }
        }

        public bool IsDeduction
        {
            get
            {
                return this._IsDeduction;
            }
            set
            {
                if ((this._IsDeduction != value))
                {
                    this._IsDeduction = value;
                }
            }
        }


        public bool IsDelete
        {
            get
            {
                return this._IsDelete;
            }
            set
            {
                if ((this._IsDelete != value))
                {
                    this._IsDelete = value;
                }
            }
        }
    }
}