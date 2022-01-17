import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-char-info',
  templateUrl: './char-info.component.html',
  styleUrls: ['./char-info.component.css']
})
export class CharInfoComponent implements OnInit {
  public characters: CharacterInfo[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<CharacterInfo[]>(baseUrl + 'api/character').subscribe(result => {
      this.characters = result;
      console.log(result);
    },
      error => console.error(error));
  }

  ngOnInit() {
  }

}

interface CharacterInfo {
  characterName: string;
  worldServer: string;
}
