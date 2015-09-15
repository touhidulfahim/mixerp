/********************************************************************************
Copyright (C) MixERP Inc. (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, version 2 of the License.


MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MixERP.Net.DbFactory;
using MixERP.Net.EntityParser;
using MixERP.Net.Framework;
using Npgsql;
using PetaPoco;
using Serilog;

namespace MixERP.Net.Schemas.Policy.Data
{
    /// <summary>
    /// Provides simplified data access features to perform SCRUD operation on the database table "policy.voucher_verification_policy".
    /// </summary>
    public class VoucherVerificationPolicy : DbAccess
    {
        /// <summary>
        /// The schema of this table. Returns literal "policy".
        /// </summary>
	    public override string ObjectNamespace => "policy";

        /// <summary>
        /// The schema unqualified name of this table. Returns literal "voucher_verification_policy".
        /// </summary>
	    public override string ObjectName => "voucher_verification_policy";

        /// <summary>
        /// Login id of application user accessing this table.
        /// </summary>
		public long LoginId { get; set; }

        /// <summary>
        /// The name of the database on which queries are being executed to.
        /// </summary>
        public string Catalog { get; set; }

		/// <summary>
		/// Performs SQL count on the table "policy.voucher_verification_policy".
		/// </summary>
		/// <returns>Returns the number of rows of the table "policy.voucher_verification_policy".</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public long Count()
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return 0;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to count entity \"VoucherVerificationPolicy\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT COUNT(*) FROM policy.voucher_verification_policy;";
			return Factory.Scalar<long>(this.Catalog, sql);
		}

		/// <summary>
		/// Executes a select query on the table "policy.voucher_verification_policy" with a where filter on the column "policy_id" to return a single instance of the "VoucherVerificationPolicy" class. 
		/// </summary>
		/// <param name="policyId">The column "policy_id" parameter used on where filter.</param>
		/// <returns>Returns a non-live, non-mapped instance of "VoucherVerificationPolicy" class mapped to the database row.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public MixERP.Net.Entities.Policy.VoucherVerificationPolicy Get(int policyId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the get entity \"VoucherVerificationPolicy\" filtered by \"PolicyId\" with value {PolicyId} was denied to the user with Login ID {LoginId}", policyId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT * FROM policy.voucher_verification_policy WHERE policy_id=@0;";
			return Factory.Get<MixERP.Net.Entities.Policy.VoucherVerificationPolicy>(this.Catalog, sql, policyId).FirstOrDefault();
		}

        /// <summary>
        /// Custom fields are user defined form elements for policy.voucher_verification_policy.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection for the table policy.voucher_verification_policy</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<PetaPoco.CustomField> GetCustomFields(string resourceId)
        {
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to get custom fields for entity \"VoucherVerificationPolicy\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            string sql;
			if (string.IsNullOrWhiteSpace(resourceId))
            {
				sql = "SELECT * FROM core.custom_field_definition_view WHERE table_name='policy.voucher_verification_policy' ORDER BY field_order;";
				return Factory.Get<PetaPoco.CustomField>(this.Catalog, sql);
            }

            sql = "SELECT * from core.get_custom_field_definition('policy.voucher_verification_policy'::text, @0::text) ORDER BY field_order;";
			return Factory.Get<PetaPoco.CustomField>(this.Catalog, sql, resourceId);
        }

        /// <summary>
        /// Displayfields provide a minimal name/value context for data binding the row collection of policy.voucher_verification_policy.
        /// </summary>
        /// <returns>Returns an enumerable name and value collection for the table policy.voucher_verification_policy</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public IEnumerable<DisplayField> GetDisplayFields()
		{
			List<DisplayField> displayFields = new List<DisplayField>();

			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return displayFields;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to get display field for entity \"VoucherVerificationPolicy\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT policy_id AS key, policy_id as value FROM policy.voucher_verification_policy;";
			using (NpgsqlCommand command = new NpgsqlCommand(sql))
			{
				using (DataTable table = DbOperation.GetDataTable(this.Catalog, command))
				{
					if (table?.Rows == null || table.Rows.Count == 0)
					{
						return displayFields;
					}

					foreach (DataRow row in table.Rows)
					{
						if (row != null)
						{
							DisplayField displayField = new DisplayField
							{
								Key = row["key"].ToString(),
								Value = row["value"].ToString()
							};

							displayFields.Add(displayField);
						}
					}
				}
			}

			return displayFields;
		}

		/// <summary>
		/// Inserts or updates the instance of VoucherVerificationPolicy class on the database table "policy.voucher_verification_policy".
		/// </summary>
		/// <param name="voucherVerificationPolicy">The instance of "VoucherVerificationPolicy" class to insert or update.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void AddOrEdit(MixERP.Net.Entities.Policy.VoucherVerificationPolicy voucherVerificationPolicy)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

			if(voucherVerificationPolicy.PolicyId > 0){
				this.Update(voucherVerificationPolicy, voucherVerificationPolicy.PolicyId);
				return;
			}
	
			this.Add(voucherVerificationPolicy);
		}

		/// <summary>
		/// Inserts the instance of VoucherVerificationPolicy class on the database table "policy.voucher_verification_policy".
		/// </summary>
		/// <param name="voucherVerificationPolicy">The instance of "VoucherVerificationPolicy" class to insert.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void Add(MixERP.Net.Entities.Policy.VoucherVerificationPolicy voucherVerificationPolicy)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Create, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to add entity \"VoucherVerificationPolicy\" was denied to the user with Login ID {LoginId}. {VoucherVerificationPolicy}", this.LoginId, voucherVerificationPolicy);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			Factory.Insert(this.Catalog, voucherVerificationPolicy);
		}

		/// <summary>
		/// Updates the row of the table "policy.voucher_verification_policy" with an instance of "VoucherVerificationPolicy" class against the primary key value.
		/// </summary>
		/// <param name="voucherVerificationPolicy">The instance of "VoucherVerificationPolicy" class to update.</param>
		/// <param name="policyId">The value of the column "policy_id" which will be updated.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void Update(MixERP.Net.Entities.Policy.VoucherVerificationPolicy voucherVerificationPolicy, int policyId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Edit, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to edit entity \"VoucherVerificationPolicy\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}. {VoucherVerificationPolicy}", policyId, this.LoginId, voucherVerificationPolicy);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			Factory.Update(this.Catalog, voucherVerificationPolicy, policyId);
		}

		/// <summary>
		/// Deletes the row of the table "policy.voucher_verification_policy" against the primary key value.
		/// </summary>
		/// <param name="policyId">The value of the column "policy_id" which will be deleted.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void Delete(int policyId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Delete, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to delete entity \"VoucherVerificationPolicy\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}.", policyId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "DELETE FROM policy.voucher_verification_policy WHERE policy_id=@0;";
			Factory.NonQuery(this.Catalog, sql, policyId);
		}

		/// <summary>
		/// Performs a select statement on table "policy.voucher_verification_policy" producing a paged result of 25.
		/// </summary>
		/// <returns>Returns the first page of collection of "VoucherVerificationPolicy" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public IEnumerable<MixERP.Net.Entities.Policy.VoucherVerificationPolicy> GetPagedResult()
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the first page of the entity \"VoucherVerificationPolicy\" was denied to the user with Login ID {LoginId}.", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT * FROM policy.voucher_verification_policy ORDER BY policy_id LIMIT 25 OFFSET 0;";
			return Factory.Get<MixERP.Net.Entities.Policy.VoucherVerificationPolicy>(this.Catalog, sql);
		}

		/// <summary>
		/// Performs a select statement on table "policy.voucher_verification_policy" producing a paged result of 25.
		/// </summary>
		/// <param name="pageNumber">Enter the page number to produce the paged result.</param>
		/// <returns>Returns collection of "VoucherVerificationPolicy" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public IEnumerable<MixERP.Net.Entities.Policy.VoucherVerificationPolicy> GetPagedResult(long pageNumber)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the entity \"VoucherVerificationPolicy\" was denied to the user with Login ID {LoginId}.", pageNumber, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			long offset = (pageNumber -1) * 25;
			const string sql = "SELECT * FROM policy.voucher_verification_policy ORDER BY policy_id LIMIT 25 OFFSET @0;";
				
			return Factory.Get<MixERP.Net.Entities.Policy.VoucherVerificationPolicy>(this.Catalog, sql, offset);
		}

        /// <summary>
		/// Performs a filtered select statement on table "policy.voucher_verification_policy" producing a paged result of 25.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the paged result.</param>
        /// <param name="filters">The list of filter conditions.</param>
		/// <returns>Returns collection of "VoucherVerificationPolicy" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Policy.VoucherVerificationPolicy> GetWhere(long pageNumber, List<EntityParser.Filter> filters)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the filtered entity \"VoucherVerificationPolicy\" was denied to the user with Login ID {LoginId}. Filters: {Filters}.", pageNumber, this.LoginId, filters);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            long offset = (pageNumber - 1) * 25;
            Sql sql = Sql.Builder.Append("SELECT * FROM policy.voucher_verification_policy WHERE 1 = 1");

            MixERP.Net.EntityParser.Data.Service.AddFilters(ref sql, new MixERP.Net.Entities.Policy.VoucherVerificationPolicy(), filters);

            sql.OrderBy("policy_id");
            sql.Append("LIMIT @0", 25);
            sql.Append("OFFSET @0", offset);

            return Factory.Get<MixERP.Net.Entities.Policy.VoucherVerificationPolicy>(this.Catalog, sql);
        }

        public IEnumerable<MixERP.Net.Entities.Policy.VoucherVerificationPolicy> Get(int[] policyIds)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to entity \"VoucherVerificationPolicy\" was denied to the user with Login ID {LoginId}. policyIds: {policyIds}.", this.LoginId, policyIds);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

			const string sql = "SELECT * FROM policy.voucher_verification_policy WHERE policy_id IN (@0);";

            return Factory.Get<MixERP.Net.Entities.Policy.VoucherVerificationPolicy>(this.Catalog, sql, policyIds);
        }

	}
}