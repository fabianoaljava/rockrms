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
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace Rock.Api.Cms
{
	[AspNetCompatibilityRequirements( RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed )]
    public partial class BlockInstanceService : IBlockInstanceService
    {
		[WebGet( UriTemplate = "{id}" )]
        public Rock.Models.Cms.BlockInstance Get( string id )
        {
            var currentUser = System.Web.Security.Membership.GetUser();
            if ( currentUser == null )
                throw new FaultException( "Must be logged in" );

            using (Rock.Helpers.UnitOfWorkScope uow = new Rock.Helpers.UnitOfWorkScope())
            {
                uow.objectContext.Configuration.ProxyCreationEnabled = false;
				Rock.Services.Cms.BlockInstanceService BlockInstanceService = new Rock.Services.Cms.BlockInstanceService();
                Rock.Models.Cms.BlockInstance BlockInstance = BlockInstanceService.Get( int.Parse( id ) );
                if ( BlockInstance.Authorized( "View", currentUser ) )
                    return BlockInstance;
                else
                    throw new FaultException( "Unauthorized" );
            }
        }
		
		[WebInvoke( Method = "PUT", UriTemplate = "{id}" )]
        public void UpdateBlockInstance( string id, Rock.Models.Cms.BlockInstance BlockInstance )
        {
            var currentUser = System.Web.Security.Membership.GetUser();
            if ( currentUser == null )
                throw new FaultException( "Must be logged in" );

            using ( Rock.Helpers.UnitOfWorkScope uow = new Rock.Helpers.UnitOfWorkScope() )
            {
                uow.objectContext.Configuration.ProxyCreationEnabled = false;

                Rock.Services.Cms.BlockInstanceService BlockInstanceService = new Rock.Services.Cms.BlockInstanceService();
                Rock.Models.Cms.BlockInstance existingBlockInstance = BlockInstanceService.Get( int.Parse( id ) );
                if ( existingBlockInstance.Authorized( "Edit", currentUser ) )
                {
                    uow.objectContext.Entry(existingBlockInstance).CurrentValues.SetValues(BlockInstance);
                    BlockInstanceService.Save( existingBlockInstance, ( int )currentUser.ProviderUserKey );
                }
                else
                    throw new FaultException( "Unauthorized" );
            }
        }

		[WebInvoke( Method = "POST", UriTemplate = "" )]
        public void CreateBlockInstance( Rock.Models.Cms.BlockInstance BlockInstance )
        {
            var currentUser = System.Web.Security.Membership.GetUser();
            if ( currentUser == null )
                throw new FaultException( "Must be logged in" );

            using ( Rock.Helpers.UnitOfWorkScope uow = new Rock.Helpers.UnitOfWorkScope() )
            {
                uow.objectContext.Configuration.ProxyCreationEnabled = false;

                Rock.Services.Cms.BlockInstanceService BlockInstanceService = new Rock.Services.Cms.BlockInstanceService();
                BlockInstanceService.Add( BlockInstance );
                BlockInstanceService.Save( BlockInstance, ( int )currentUser.ProviderUserKey );
            }
        }

		[WebInvoke( Method = "DELETE", UriTemplate = "{id}" )]
        public void DeleteBlockInstance( string id )
        {
            var currentUser = System.Web.Security.Membership.GetUser();
            if ( currentUser == null )
                throw new FaultException( "Must be logged in" );

            using ( Rock.Helpers.UnitOfWorkScope uow = new Rock.Helpers.UnitOfWorkScope() )
            {
                uow.objectContext.Configuration.ProxyCreationEnabled = false;

                Rock.Services.Cms.BlockInstanceService BlockInstanceService = new Rock.Services.Cms.BlockInstanceService();
                Rock.Models.Cms.BlockInstance BlockInstance = BlockInstanceService.Get( int.Parse( id ) );
                if ( BlockInstance.Authorized( "Edit", currentUser ) )
                {
                    BlockInstanceService.Delete( BlockInstance );
                }
                else
                    throw new FaultException( "Unauthorized" );
            }
        }

    }
}
