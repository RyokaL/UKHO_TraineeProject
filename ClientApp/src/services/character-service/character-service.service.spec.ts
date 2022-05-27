import { TestBed } from "@angular/core/testing";

import { CharacterService } from "./character-service.service";
import {HttpClientTestingModule} from '@angular/common/http/testing'

describe('CharacterServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({ imports: [HttpClientTestingModule]}));

  it('should be created', () => {
    const service: CharacterService = TestBed.get(CharacterService);
    expect(service).toBeTruthy();
  });
});
