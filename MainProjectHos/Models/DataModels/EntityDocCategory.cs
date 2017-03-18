using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityDocCategory
    /// </summary>
    public class EntityDocCategory
    {
        public EntityDocCategory()
        {

        }

        private int _DocAllocId;

        private int _OperaCatId;

        private int _DocId;

        private bool _IsDelete;

        public string Doc_Name { get; set; }

        public string Opera_Name { get; set; }

        public int DocAllocId
        {
            get
            {
                return this._DocAllocId;
            }
            set
            {
                if ((this._DocAllocId != value))
                {
                    this._DocAllocId = value;
                }
            }
        }

        public int OperaCatId
        {
            get
            {
                return this._OperaCatId;
            }
            set
            {
                if ((this._OperaCatId != value))
                {
                    this._OperaCatId = value;
                }
            }
        }

        public int DocId
        {
            get
            {
                return this._DocId;
            }
            set
            {
                if ((this._DocId != value))
                {
                    this._DocId = value;
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

        public decimal Charges { get; internal set; }
    }
}