import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CharacterService } from 'src/services/character-service/character-service.service';
import { ParseLogService } from 'src/services/parse-log-service/parse-log.service';

import { ParseLogComponent } from './parse-log.component';

describe('ParseLogComponent', () => {
  let component: ParseLogComponent;
  let fixture: ComponentFixture<ParseLogComponent>;
  let mockCharacterService = {};
  let mockParseLogService = {};

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParseLogComponent ],
      providers: [ { provide: CharacterService, useValue: mockCharacterService },
      { provide: ParseLogService, useValue: mockParseLogService } ]
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
