import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CharacterInfo } from "../../services/character-service/character-service.service";

@Component({
  selector: 'app-parse-log',
  templateUrl: './parse-log.component.html',
  styleUrls: ['./parse-log.component.css']
})
export class ParseLogComponent implements OnInit {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

  }

  ngOnInit() {
  }

}

export interface CharacterLogParse {
  jobClass: string;
  raidDPS: number;
  actualDPS: number;
  totalDamage: number;
  percentActive: number;
  hps: number;
  overhealPercent: number;
  damageTaken: number;
  character: CharacterInfo;
}

export interface LogParse {
  instanceName: string;
  timeTaken: number;
  succeeded: boolean;
  dateUploaded: Date;
  characterLogs: CharacterLogParse[];
}
