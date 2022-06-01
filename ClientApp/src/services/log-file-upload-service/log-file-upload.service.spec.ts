import { TestBed } from '@angular/core/testing';

import { LogFileUploadService } from './log-file-upload.service';

describe('LogFileUploadService', () => {
  let service: LogFileUploadService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LogFileUploadService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
