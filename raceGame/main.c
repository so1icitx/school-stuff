#include <stdio.h>
#include <stdlib.h>
#include <time.h>

// Structures section
typedef struct{
  int numberOfLaps;
  int currentLap;
  char* firstPlaceDriverName;
  char* firstPlaceCarColor;
}Race;

typedef struct{
  char* driverName;
  char* raceCarColor;
  int totalLapTime;
}RaceCar;
// Print functions section
void printIntro(){
  printf("Welcome to our main event digital race fans! I hope everybody has their snacks because we are about to begin!\n");
}
void printCountDown(){
  printf("Racers Ready! In...\n");
  for (int i = 5; i > 0; i--){
    printf("%d\n", i);
  }
  printf("Race!\n");
}
void printFirstPlaceAfterLap(Race* race){
  printf("After lap number %d\n", (*race).currentLap);
  printf("First Place is: %s in the %s car!\n", (*race).firstPlaceDriverName, race->firstPlaceCarColor);
}
void printCongratulations(Race* race){
  printf("Let's all congratulate %s in the %s race car for an amazing performance.\nIt truly was a great race and everybody have a goodnight!", race->firstPlaceDriverName, race->firstPlaceCarColor);
}
// Logic functions section
int calculateTimeToCompleteLap(){
  int speed = (rand() % 3) + 1;
  int acceleration = (rand() % 3) + 1;
  int nerves = (rand() % 3) + 1;
  return speed + acceleration+ nerves;
}
void updateRaceCar(RaceCar* raceCar){
  raceCar -> totalLapTime += calculateTimeToCompleteLap();
}
void updateFirstPlace(Race* race, RaceCar* raceCar1, RaceCar* raceCar2){
  if(raceCar1->totalLapTime <= raceCar2->totalLapTime){
    race->firstPlaceDriverName = raceCar1->driverName;
    race->firstPlaceCarColor = raceCar1->raceCarColor;
  }
  else{
    race->firstPlaceDriverName = raceCar2->driverName;
    race->firstPlaceCarColor = raceCar2->raceCarColor;
  }
}

void startRace(RaceCar* raceCar1, RaceCar* raceCar2){
  Race race = {5, 1, "", ""};
  for(int i=0; i<race.numberOfLaps; i++){
    updateRaceCar(raceCar1);
    updateRaceCar(raceCar2);
    updateFirstPlace(&race, raceCar1, raceCar2);
    printFirstPlaceAfterLap(&race);
  }
  printCongratulations(&race);
}
int main() {
	srand(time(0));
  printIntro();
  printCountDown();
  RaceCar ferrari = {"Atila Tair", "Black", 0};
  RaceCar lambo = {"Jiji Tair", "Yellow", 0};
  startRace(&ferrari, &lambo);
  
};
