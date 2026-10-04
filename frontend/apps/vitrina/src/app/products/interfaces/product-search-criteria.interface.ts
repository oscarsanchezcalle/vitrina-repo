export interface ProductSearchCriteria {
  searchTerm?: string | null;
  isActive?: boolean | null;
  pageNumber: number;
  pageSize: number;
}
