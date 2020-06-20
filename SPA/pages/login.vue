<template>
  <v-layout column justify-center align-center>
    <v-flex>
      <v-form v-model="valid">
        <v-row>
          <v-text-field
            v-model="login.username"
            :rules="rules"
            label="Usuário"
            prepend-icon="mdi-account"
            required
          ></v-text-field>
        </v-row>
        <v-row>
          <v-text-field
            v-model="login.password"
            :rules="rules"
            label="Senha"
            type="password"
            prepend-icon="mdi-form-textbox-password"
            required
          ></v-text-field>
        </v-row>
        <v-row>
          <v-btn
            :disabled="login.username == null || login.password == null"
            :loading="loading"
            @click="authenticate"
          >
            Login
          </v-btn>
        </v-row>
      </v-form>
    </v-flex>
  </v-layout>
</template>

<script>
export default {
  data: () => ({
    valid: false,
    login: {
      username: null,
      password: null
    },
    loading: false,
    rules: [(v) => !!v || 'Campo é obrigatório']
  }),
  methods: {
    async authenticate() {
      this.loading = true
      let authResult = null
      try {
        authResult = await this.$auth.loginWith('local', {
          data: this.login
        })
        this.$auth.setUser(authResult.data)
        this.$toast.success('Login efetuado')
      } catch (err) {
        this.$toast.error(err, {
          duration: null,
          action: {
            text: 'Fechar',
            onClick: (_, toastObject) => {
              toastObject.goAway(0)
            }
          }
        })
      }

      this.loading = false
    }
  }
}
</script>
