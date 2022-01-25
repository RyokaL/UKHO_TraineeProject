import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ParseLogComponent } from './parse-log.component';

describe('ParseLogComponent', () => {
  let component: ParseLogComponent;
  let fixture: ComponentFixture<ParseLogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParseLogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParseLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
