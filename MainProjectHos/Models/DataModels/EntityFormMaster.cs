using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntityFormMaster
    /// </summary>
    public class EntityFormMaster
    {
        public EntityFormMaster()
        {

        }

        private int _FormId;

        private string _FormName;

        private string _FormTitle;

        private bool _IsDelete;

        private int _AuthenticationId;

        private int _EmpId;

        public int FormId
        {
            get
            {
                return this._FormId;
            }
            set
            {
                if ((this._FormId != value))
                {
                    this._FormId = value;
                }
            }
        }

        public string FormName
        {
            get
            {
                return this._FormName;
            }
            set
            {
                if ((this._FormName != value))
                {
                    this._FormName = value;
                }
            }
        }

        public string FormTitle
        {
            get
            {
                return this._FormTitle;
            }
            set
            {
                if ((this._FormTitle != value))
                {
                    this._FormTitle = value;
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
        public int AuthenticationId
        {
            get
            {
                return this._AuthenticationId;
            }
            set
            {
                if ((this._AuthenticationId != value))
                {
                    this._AuthenticationId = value;
                }
            }
        }

        public int EmpId
        {
            get
            {
                return this._EmpId;
            }
            set
            {
                if ((this._EmpId != value))
                {
                    this._EmpId = value;
                }
            }
        }
    }
}