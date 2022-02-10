import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserLoginOutNavComponent } from './user-login-out-nav.component';

describe('UserLoginOutNavComponent', () => {
  let component: UserLoginOutNavComponent;
  let fixture: ComponentFixture<UserLoginOutNavComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserLoginOutNavComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserLoginOutNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
