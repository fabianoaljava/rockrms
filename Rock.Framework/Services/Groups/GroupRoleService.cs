//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the T4\Model.tt template.
//
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rock.Models.Groups;
using Rock.Repository.Groups;

namespace Rock.Services.Groups
{
    public partial class GroupRoleService : Rock.Services.Service<Rock.Models.Groups.GroupRole>
    {
        public IEnumerable<Rock.Models.Groups.GroupRole> GetByGuid( Guid guid )
        {
            return Repository.Find( t => t.Guid == guid );
        }
		
        public IEnumerable<Rock.Models.Groups.GroupRole> GetByOrder( int? order )
        {
            return Repository.Find( t => ( t.Order == order || ( order == null && t.Order == null ) ) );
        }
		
    }
}
