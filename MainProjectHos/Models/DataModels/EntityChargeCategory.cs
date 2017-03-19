using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityChargeCategory
    /// </summary>
    public class EntityChargeCategory
    {
        public EntityChargeCategory()
        {

        }

        private int _ChargesId;

        private string _ChargeCategoryName;

        private bool _IsOperation;

        private bool _IsBed;

        private bool _IsConsulting;

        private bool _IsOther;

        private bool _IsDelete;

        public bool IsICU { get; set; }


        public int ChargesId
        {
            get
            {
                return this._ChargesId;
            }
            set
            {
                if ((this._ChargesId != value))
                {
                    this._ChargesId = value;
                }
            }
        }

        public string ChargeCategoryName
        {
            get
            {
                return this._ChargeCategoryName;
            }
            set
            {
                if ((this._ChargeCategoryName != value))
                {
                    this._ChargeCategoryName = value;
                }
            }
        }

        public bool IsOperation
        {
            get
            {
                return this._IsOperation;
            }
            set
            {
                if ((this._IsOperation != value))
                {
                    this._IsOperation = value;
                }
            }
        }

        public bool IsBed
        {
            get
            {
                return this._IsBed;
            }
            set
            {
                if ((this._IsBed != value))
                {
                    this._IsBed = value;
                }
            }
        }

        public bool IsConsulting
        {
            get
            {
                return this._IsConsulting;
            }
            set
            {
                if ((this._IsConsulting != value))
                {
                    this._IsConsulting = value;
                }
            }
        }

        public bool IsOther
        {
            get
            {
                return this._IsOther;
            }
            set
            {
                if ((this._IsOther != value))
                {
                    this._IsOther = value;
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

        public bool IsNursing { get; internal set; }
        public bool IsRMO { get; internal set; }
    }
}