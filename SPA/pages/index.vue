<template>
  <v-layout column justify-center align-center>
    <v-flex xs12 sm8 md6>
      <div class="text-center">
        <totvs-logo />
      </div>
      <v-card>
        <v-card-title class="headline">
          Frontend do desafio fullstack TOTVS
        </v-card-title>
        <v-card-text>
          <p>
            Para o frontend foi utilizado o Nuxt + Vuetify.
          </p>
          <p>
            Nuxt é um framework para construção de aplicações com Vue com foco
            na experiência do desenvolvedor.
          </p>
          <p>
            Já o Vuetify é uma biblioteca que implementa o Material Design em
            componentes do Vue.
          </p>
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn color="primary" nuxt to="/auction">
            Ir para o CRUD
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import TotvsLogo from '~/components/TotvsLogo.vue'

export default {
  components: {
    TotvsLogo
  }
}
</script>
