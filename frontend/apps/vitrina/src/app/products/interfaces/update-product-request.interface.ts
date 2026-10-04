import { CreateProductRequest } from './create-product-request.interface';

export interface UpdateProductRequest extends CreateProductRequest {
  id: number;
}
