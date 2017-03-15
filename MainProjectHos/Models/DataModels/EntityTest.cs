using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntityTest
    /// </summary>
    public class EntityTest
    {
        public EntityTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _TestId;

        private string _TestName;

        private System.Nullable<decimal> _TestCharge;

        private string _Precautions;

        private bool _IsDelete;

        private System.Nullable<System.DateTime> _CreateDate;

        private System.Nullable<int> _CreatedBy;

        public int TestId
        {
            get
            {
                return this._TestId;
            }
            set
            {
                if ((this._TestId != value))
                {
                    this._TestId = value;
                }
            }
        }

        public string TestName
        {
            get
            {
                return this._TestName;
            }
            set
            {
                if ((this._TestName != value))
                {
                    this._TestName = value;
                }
            }
        }

        public System.Nullable<decimal> TestCharge
        {
            get
            {
                return this._TestCharge;
            }
            set
            {
                if ((this._TestCharge != value))
                {
                    this._TestCharge = value;
                }
            }
        }

        public string Precautions
        {
            get
            {
                return this._Precautions;
            }
            set
            {
                if ((this._Precautions != value))
                {
                    this._Precautions = value;
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

        public System.Nullable<System.DateTime> CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                if ((this._CreateDate != value))
                {
                    this._CreateDate = value;
                }
            }
        }

        public System.Nullable<int> CreatedBy
        {
            get
            {
                return this._CreatedBy;
            }
            set
            {
                if ((this._CreatedBy != value))
                {
                    this._CreatedBy = value;
                }
            }
        }

        public bool IsRadiology { get; set; }

        public bool IsPathology { get; set; }

        public int TestCatId { get; set; }

        public string TestCatagoryName { get; set; }

    }
}