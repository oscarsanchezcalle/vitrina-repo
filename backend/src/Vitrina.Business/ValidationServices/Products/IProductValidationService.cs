using Vitrina.Dto.Common;
using Vitrina.Dto.Products;

namespace Vitrina.Business.ValidationServices;

/// <summary>
/// Defines product validation rules.
/// </summary>
public interface IProductValidationService
{
	/// <summary>
	/// Validates the create criteria.
	/// </summary>
	/// <param name="criteria">The create criteria.</param>
	/// <returns>The validation result.</returns>
	ValidationResultDto ValidateCreateCriteria(CreateProductCriteriaDto? criteria);

	/// <summary>
	/// Validates the update criteria.
	/// </summary>
	/// <param name="criteria">The update criteria.</param>
	/// <returns>The validation result.</returns>
	ValidationResultDto ValidateUpdateCriteria(UpdateProductCriteriaDto? criteria);

	/// <summary>
	/// Validates the search criteria.
	/// </summary>
	/// <param name="criteria">The search criteria.</param>
	/// <returns>The validation result.</returns>
	ValidationResultDto ValidateSearchCriteria(ProductSearchCriteriaDto? criteria);
}
