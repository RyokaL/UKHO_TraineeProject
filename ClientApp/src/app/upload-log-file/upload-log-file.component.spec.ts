import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadLogFileComponent } from './upload-log-file.component';

describe('UploadLogFileComponent', () => {
  let component: UploadLogFileComponent;
  let fixture: ComponentFixture<UploadLogFileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UploadLogFileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadLogFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
