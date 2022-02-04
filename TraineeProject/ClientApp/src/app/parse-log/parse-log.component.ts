import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { CharacterInfo, CharacterService } from "../../services/character-service/character-service.service";
import { LogParse, ParseLogService } from "../../services/parse-log-service/parse-log.service";

@Component({
  selector: 'app-parse-log',
  templateUrl: './parse-log.component.html',
  styleUrls: ['./parse-log.component.css']
})
export class ParseLogComponent implements OnInit {

  private id: number;
  public parses: LogParse[];
  public characterName: string;
  public showLogs: boolean[] = [];

  constructor(http: HttpClient,
    private route: ActivatedRoute,
    private parseService: ParseLogService,
    private charService: CharacterService,
    @Inject('BASE_URL') baseUrl: string) {

  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.id = Number(params.get("id"));
      this.updateParses();
    });
  }
  
  updateParses() {
    this.parses = null;
    this.charService.getCharacterById(this.id).subscribe(result => this.characterName = result.characterName);
    this.parseService.getLogsForCharacter(this.id).subscribe(
      result => {
        this.parses = result;
        this.showLogs = this.showLogs.fill(false, 0, this.parses.length);
        console.log(JSON.stringify(this.parses));
      }
    );
  }
}
