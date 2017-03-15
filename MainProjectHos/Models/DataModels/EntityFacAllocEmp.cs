using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntityFacAllocEmp
    /// </summary>
    public class EntityFacAllocEmp
    {
        public EntityFacAllocEmp()
        {

        }
        private int _EmpAllocation_Id;

        private int _Emp_Id;

        private int _Shift_Id;

        private bool _IsDelete;

        public string FullName { get; set; }

        public int EmpAllocation_Id
        {
            get
            {
                return this._EmpAllocation_Id;
            }
            set
            {
                if ((this._EmpAllocation_Id != value))
                {
                    this._EmpAllocation_Id = value;
                }
            }
        }

        public int Emp_Id
        {
            get
            {
                return this._Emp_Id;
            }
            set
            {
                if ((this._Emp_Id != value))
                {
                    this._Emp_Id = value;
                }
            }
        }

        public int Shift_Id
        {
            get
            {
                return this._Shift_Id;
            }
            set
            {
                if ((this._Shift_Id != value))
                {
                    this._Shift_Id = value;
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