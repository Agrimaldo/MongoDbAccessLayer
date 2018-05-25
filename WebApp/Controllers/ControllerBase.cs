using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Rule;
using WebApp.Rule.Util;

namespace WebApp.Controllers
{
    public class ControllerBase : Controller
    {
        #region Properties
        AlertType AlertType
        {
            get
            {
                if (TempData["AlertType"] != null)
                {
                    return (AlertType)TempData["AlertType"].ToString().GetEnumIndex<AlertType>();
                }
                return AlertType.Default;
            }
            set
            {
                TempData["AlertType"] = value.GetDescription();
            }
        }

        string AlertMessage
        {
            get
            {
                if (TempData["AlertMessage"] != null)
                {
                    return TempData["AlertMessage"].ToString();
                }
                return string.Empty;
            }
            set
            {
                TempData["AlertMessage"] = value;
            }
        }

        protected Repository Repository { get; set; }
        #endregion

        #region Constructors
        public ControllerBase() => Repository = new Repository();

        #endregion

        #region Methods
        protected void Alert(AlertType _type, string _message)
        {
            this.AlertType = _type;
            this.AlertMessage = _message;
        }
        #endregion
    }
}