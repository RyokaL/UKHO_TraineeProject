import { HttpClientTestingModule } from "@angular/common/http/testing";
import { TestBed } from "@angular/core/testing";

import { ParseLogService } from "./parse-log.service";

describe('ParseLogService', () => {
  beforeEach(() => TestBed.configureTestingModule({ imports: [HttpClientTestingModule]}));

  it('should be created', () => {
    const service: ParseLogService = TestBed.get(ParseLogService);
    expect(service).toBeTruthy();
  });
});
