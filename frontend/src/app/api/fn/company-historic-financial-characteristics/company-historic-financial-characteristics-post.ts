/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { CompanyHistoricFinancialCharacteristicCreateDto } from '../../models/company-historic-financial-characteristic-create-dto';

export interface CompanyHistoricFinancialCharacteristicsPost$Params {
      body?: CompanyHistoricFinancialCharacteristicCreateDto
}

export function companyHistoricFinancialCharacteristicsPost(http: HttpClient, rootUrl: string, params?: CompanyHistoricFinancialCharacteristicsPost$Params, context?: HttpContext): Observable<StrictHttpResponse<void>> {
  const rb = new RequestBuilder(rootUrl, companyHistoricFinancialCharacteristicsPost.PATH, 'post');
  if (params) {
    rb.body(params.body, 'application/json');
  }

  return http.request(
    rb.build({ responseType: 'text', accept: '*/*', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
    })
  );
}

companyHistoricFinancialCharacteristicsPost.PATH = '/CompanyHistoricFinancialCharacteristics';
