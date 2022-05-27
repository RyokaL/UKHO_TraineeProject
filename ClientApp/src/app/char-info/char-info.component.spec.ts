import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CharacterService } from 'src/services/character-service/character-service.service';

import { CharInfoComponent } from './char-info.component';

describe('CharInfoComponent', () => {
  let component: CharInfoComponent;
  let fixture: ComponentFixture<CharInfoComponent>;
  let mockCharacterService = {};

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CharInfoComponent ],
      providers: [ { provide: CharacterService, useValue: mockCharacterService } ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CharInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
