import { ReactiveFormsModule } from "@angular/forms";

export interface Pagination {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
}

export class PaginatedResult<T>{
    result!: T;
    pagination!: Pagination;
}