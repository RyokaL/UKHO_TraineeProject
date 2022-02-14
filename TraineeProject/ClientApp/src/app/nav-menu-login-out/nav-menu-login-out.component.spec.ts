import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavMenuLoginOutComponent } from './nav-menu-login-out.component';

describe('NavMenuLoginOutComponent', () => {
  let component: NavMenuLoginOutComponent;
  let fixture: ComponentFixture<NavMenuLoginOutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavMenuLoginOutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavMenuLoginOutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
