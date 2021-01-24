using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaSati.Models
{
	 public enum SatnicaStatusEnum
	 {
		  WAITING_APPROVAL = 1,
		  REVISION_NEEDED = 2,
		  APPROVED = 3,
		  WAITING_SUBMIT = 4
	 }
}