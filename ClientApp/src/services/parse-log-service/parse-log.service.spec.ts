import { TestBed } from "@angular/core/testing";

import { ParseLogService } from "./parse-log.service";

describe('ParseLogService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ParseLogService = TestBed.get(ParseLogService);
    expect(service).toBeTruthy();
  });
});
