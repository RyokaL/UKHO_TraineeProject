import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MsalBroadcastService, MsalGuardConfiguration, MsalService, MSAL_GUARD_CONFIG } from '@azure/msal-angular';
import { CharacterInfo, CharacterInfoReq, CharacterService } from 'src/services/character-service/character-service.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  constructor(
    @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
    public authService: MsalService,
    private msalBroadcastService: MsalBroadcastService,
    private formBuilder: FormBuilder,
    private charService: CharacterService
  ) { }

    error = false;
    public characters: CharacterInfo[];

  addCharacterForm = this.formBuilder.group({
    characterName: '',
    worldServer: ''
  });

  onSubmit(): void {
    this.error = this.charService.addUserToCharacter(this.addCharacterForm.value as CharacterInfoReq);
    this.addCharacterForm.reset();
  }

  setPrivacy(event, character: CharacterInfo): void {
    this.charService.updatePrivacy({characterName: character.characterName, worldServer: character.worldServer}, event.target.checked);
  }

  ngOnInit(): void {
    this.charService.getCharactersByUserId().subscribe(result => this.characters = result);
  }
}

