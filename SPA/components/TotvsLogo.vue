<template>
  <img class="TotvsLogo" alt="Logotipo TOTVS" src="/logo.png" />
</template>

<style>
.TotvsLogo {
  transform: rotateY(560deg);
  animation: turn 3.5s ease-out forwards 1s;
}

@keyframes turn {
  100% {
    transform: rotateY(0deg);
  }
}
</style>
