using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityOperationCategory
    /// </summary>
    public class EntityOperationMaster
    {
        public EntityOperationMaster()
        {

        }

        private int _OperationId;

        private int _OperationCategoryId;

        private string _OperationName;

        private decimal _Price;

        private bool _IsDelete;

        public string CatName { get; set; }

        public int OperationId
        {
            get
            {
                return this._OperationId;
            }
            set
            {
                if ((this._OperationId != value))
                {
                    this._OperationId = value;
                }
            }
        }

        public int OperationCategoryId
        {
            get
            {
                return this._OperationCategoryId;
            }
            set
            {
                if ((this._OperationCategoryId != value))
                {
                    this._OperationCategoryId = value;
                }
            }
        }

        public string OperationName
        {
            get
            {
                return this._OperationName;
            }
            set
            {
                if ((this._OperationName != value))
                {
                    this._OperationName = value;
                }
            }
        }

        public decimal Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                if ((this._Price != value))
                {
                    this._Price = value;
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