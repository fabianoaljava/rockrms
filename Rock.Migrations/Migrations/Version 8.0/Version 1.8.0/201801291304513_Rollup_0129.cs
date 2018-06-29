// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
namespace Rock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    ///
    /// </summary>
    public partial class Rollup_0129 : Rock.Migrations.RockMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            // JE:Add Interaction Channel for CDR
            Sql( @"DECLARE @ChannelGuid uniqueidentifier
SET @ChannelGuid = CAST( 'B3904B57-62A2-57AC-43EA-94D4DEBA3D51' as uniqueidentifier )

IF NOT EXISTS( SELECT * FROM[InteractionChannel]  WHERE[Guid] = @ChannelGuid )
BEGIN
  DECLARE @InteractionEntityTypeId int = ( SELECT TOP 1[Id] FROM[EntityType] WHERE[Name] = 'Rock.Model.Person' )
  DECLARE @ChannelTypeMediumValueId int = ( SELECT TOP 1[Id] FROM[DefinedValue] WHERE[Guid] = 'B3904B57-62A2-57AC-43EA-94D4DEBA3D51')

  INSERT INTO[InteractionChannel]

    ([Name], [InteractionEntityTypeId], [ChannelTypeMediumValueId], [Guid])
  VALUES
    ( 'PBX CDR Records', @InteractionEntityTypeId, @ChannelTypeMediumValueId, @ChannelGuid )
END" );

            // JE: Update Html FieldType -> HTML
            Sql( @" 
    UPDATE [FieldType]
    SET [Name] = 'HTML'
    WHERE [Guid] = 'DD7ED4C0-A9E0-434F-ACFE-BD4F56B043DF'
" );

        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
        }
    }
}