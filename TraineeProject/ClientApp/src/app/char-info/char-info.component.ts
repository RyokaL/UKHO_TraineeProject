import { Component, OnInit } from '@angular/core';
import { CharacterInfo, CharacterService } from "../../services/character-service/character-service.service";

@Component({
  selector: 'app-char-info',
  templateUrl: './char-info.component.html',
  styleUrls: ['./char-info.component.css']
})
export class CharInfoComponent implements OnInit {
  public characters: CharacterInfo[];

  constructor(private charService: CharacterService) {

  }

  ngOnInit(): void {
    this.charService.getCharacterInfo().subscribe(result => this.characters = result);
  }

}
