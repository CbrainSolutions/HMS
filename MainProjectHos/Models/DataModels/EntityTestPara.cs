using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityTestPara
    /// </summary>
    public class EntityTestPara
    {
        public EntityTestPara()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int _TstParID;

        private int _TestId;

        private string _ParaName;

        private decimal _MinPara;

        private decimal _MaxPara;

        private bool _IsDelete;

        public string TestName { get; set; }

        public int TstParID
        {
            get
            {
                return this._TstParID;
            }
            set
            {
                if ((this._TstParID != value))
                {
                    this._TstParID = value;
                }
            }
        }

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

        public string ParaName
        {
            get
            {
                return this._ParaName;
            }
            set
            {
                if ((this._ParaName != value))
                {
                    this._ParaName = value;
                }
            }
        }

        public decimal MinPara
        {
            get
            {
                return this._MinPara;
            }
            set
            {
                if ((this._MinPara != value))
                {
                    this._MinPara = value;
                }
            }
        }

        public decimal MaxPara
        {
            get
            {
                return this._MaxPara;
            }
            set
            {
                if ((this._MaxPara != value))
                {
                    this._MaxPara = value;
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