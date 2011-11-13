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
    public partial class FileService : IFileService
    {
		[WebGet( UriTemplate = "{id}" )]
        public Rock.Models.Cms.File Get( string id )
        {
            var currentUser = System.Web.Security.Membership.GetUser();
            if ( currentUser == null )
                throw new FaultException( "Must be logged in" );

            using (Rock.Helpers.UnitOfWorkScope uow = new Rock.Helpers.UnitOfWorkScope())
            {
                uow.objectContext.Configuration.ProxyCreationEnabled = false;
				Rock.Services.Cms.FileService FileService = new Rock.Services.Cms.FileService();
                Rock.Models.Cms.File File = FileService.Get( int.Parse( id ) );
                if ( File.Authorized( "View", currentUser ) )
                    return File;
                else
                    throw new FaultException( "Unauthorized" );
            }
        }
		
		[WebInvoke( Method = "PUT", UriTemplate = "{id}" )]
        public void UpdateFile( string id, Rock.Models.Cms.File File )
        {
            var currentUser = System.Web.Security.Membership.GetUser();
            if ( currentUser == null )
                throw new FaultException( "Must be logged in" );

            using ( Rock.Helpers.UnitOfWorkScope uow = new Rock.Helpers.UnitOfWorkScope() )
            {
                uow.objectContext.Configuration.ProxyCreationEnabled = false;

                Rock.Services.Cms.FileService FileService = new Rock.Services.Cms.FileService();
                Rock.Models.Cms.File existingFile = FileService.Get( int.Parse( id ) );
                if ( existingFile.Authorized( "Edit", currentUser ) )
                {
                    uow.objectContext.Entry(existingFile).CurrentValues.SetValues(File);
                    FileService.Save( existingFile, ( int )currentUser.ProviderUserKey );
                }
                else
                    throw new FaultException( "Unauthorized" );
            }
        }

		[WebInvoke( Method = "POST", UriTemplate = "" )]
        public void CreateFile( Rock.Models.Cms.File File )
        {
            var currentUser = System.Web.Security.Membership.GetUser();
            if ( currentUser == null )
                throw new FaultException( "Must be logged in" );

            using ( Rock.Helpers.UnitOfWorkScope uow = new Rock.Helpers.UnitOfWorkScope() )
            {
                uow.objectContext.Configuration.ProxyCreationEnabled = false;

                Rock.Services.Cms.FileService FileService = new Rock.Services.Cms.FileService();
                FileService.Add( File );
                FileService.Save( File, ( int )currentUser.ProviderUserKey );
            }
        }

		[WebInvoke( Method = "DELETE", UriTemplate = "{id}" )]
        public void DeleteFile( string id )
        {
            var currentUser = System.Web.Security.Membership.GetUser();
            if ( currentUser == null )
                throw new FaultException( "Must be logged in" );

            using ( Rock.Helpers.UnitOfWorkScope uow = new Rock.Helpers.UnitOfWorkScope() )
            {
                uow.objectContext.Configuration.ProxyCreationEnabled = false;

                Rock.Services.Cms.FileService FileService = new Rock.Services.Cms.FileService();
                Rock.Models.Cms.File File = FileService.Get( int.Parse( id ) );
                if ( File.Authorized( "Edit", currentUser ) )
                {
                    FileService.Delete( File );
                }
                else
                    throw new FaultException( "Unauthorized" );
            }
        }

    }
}
