<template>
  <v-data-table
    :headers="headers"
    :items="auctions"
    :loading="loading"
    sort-by="id"
    class="elevation-1"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>Leilões</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              text
              color="blue darken-1"
              class="mb-2"
              v-bind="attrs"
              v-on="on"
              >Novo leilão</v-btn
            >
          </template>
          <v-form ref="form" v-model="valid">
            <v-card>
              <v-card-title>
                <span class="headline">{{ formTitle }}</span>
              </v-card-title>

              <v-card-text>
                <v-container>
                  <v-col>
                    <v-row>
                      <v-text-field
                        v-model="editedItem.name"
                        :rules="textRules"
                        prepend-icon="mdi-tag-text"
                        label="Nome"
                      ></v-text-field>
                    </v-row>
                    <v-row>
                      <v-text-field
                        v-model.number="editedItem.initialBid"
                        :rules="numberRules"
                        prepend-icon="mdi-currency-usd"
                        label="Valor inicial"
                        type="number"
                      ></v-text-field>
                    </v-row>
                    <v-row>
                      <v-menu
                        ref="openDateMenu"
                        v-model="auctionOpenDateDatePickerMenu"
                        :close-on-content-click="false"
                        :return-value.sync="editedItem.open"
                        transition="scale-transition"
                        offset-y
                        min-width="290px"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-text-field
                            v-model="editedItem.open"
                            :rules="textRules"
                            label="Data de abertura"
                            prepend-icon="mdi-calendar-check"
                            readonly
                            v-bind="attrs"
                            v-on="on"
                          ></v-text-field>
                        </template>
                        <v-date-picker v-model="date" no-title scrollable>
                          <v-spacer></v-spacer>
                          <v-btn
                            text
                            color="primary"
                            @click="$refs.openDateMenu.save(date)"
                            >OK</v-btn
                          >
                        </v-date-picker>
                      </v-menu>
                    </v-row>
                    <v-row>
                      <v-menu
                        ref="closeDateMenu"
                        v-model="auctionCloseDateDatePickerMenu"
                        :close-on-content-click="false"
                        :return-value.sync="editedItem.close"
                        transition="scale-transition"
                        offset-y
                        min-width="290px"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-text-field
                            v-model="editedItem.close"
                            :rules="textRules"
                            label="Data de fechamento"
                            prepend-icon="mdi-calendar-remove"
                            readonly
                            v-bind="attrs"
                            v-on="on"
                          ></v-text-field>
                        </template>
                        <v-date-picker v-model="date" no-title scrollable>
                          <v-spacer></v-spacer>
                          <v-btn
                            text
                            color="primary"
                            @click="$refs.closeDateMenu.save(date)"
                            >OK</v-btn
                          >
                        </v-date-picker>
                      </v-menu>
                    </v-row>
                    <v-row>
                      <v-checkbox
                        v-model="editedItem.isUsed"
                        label="É usado?"
                      ></v-checkbox>
                    </v-row>
                    <v-row>
                      <v-select
                        v-model="editedItem.responsible.id"
                        :items="users"
                        :rules="textRules"
                        label="Responsável"
                        item-text="name"
                        item-value="id"
                        prepend-icon="mdi-account"
                      ></v-select>
                    </v-row>
                  </v-col>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="close"
                  >Cancelar</v-btn
                >
                <v-btn
                  v-if="!isEdition"
                  :disabled="!valid"
                  color="blue darken-1"
                  text
                  type="submit"
                  @click.prevent="save"
                  >Salvar</v-btn
                >
                <v-btn
                  v-if="isEdition"
                  :disabled="!valid"
                  color="blue darken-1"
                  text
                  type="submit"
                  @click.prevent="update"
                  >Atualizar</v-btn
                >
              </v-card-actions>
            </v-card>
          </v-form>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:item.open="{ item }">{{ new Date(item.open) }}</template>
    <template v-slot:item.close="{ item }">{{ new Date(item.close) }}</template>
    <template v-slot:item.isUsed="{ item }">
      <v-simple-checkbox v-model="item.isUsed" disabled></v-simple-checkbox>
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon small class="mr-2" @click="editItem(item)">mdi-pencil</v-icon>
      <v-icon small @click="deleteItem(item)">mdi-delete</v-icon>
    </template>
    <template v-slot:no-data>
      <v-btn color="primary" @click="initialize">Atualizar</v-btn>
    </template>
  </v-data-table>
</template>

<script>
export default {
  data: () => ({
    dialog: false,
    loading: true,
    isEdition: false,
    date: null,
    valid: false,
    textRules: [(v) => !!v || 'O campo é obrigatório'],
    numberRules: [(v) => v >= 0 || 'O valor precisa ser maior ou igual a zero'],
    headers: [
      {
        text: 'ID',
        align: 'start',
        value: 'id'
      },
      { text: 'Nome', value: 'name' },
      { text: 'Valor inicial', value: 'initialBid' },
      { text: 'Abertura', value: 'open' },
      { text: 'Fechamento', value: 'close' },
      { text: 'É usado?', value: 'isUsed' },
      { text: 'Responsável', value: 'responsible.name' },
      { text: 'Ações', value: 'actions', sortable: false }
    ],
    auctions: [],
    users: [],
    auctionOpenDateDatePickerMenu: false,
    auctionCloseDateDatePickerMenu: false,
    editedItem: {
      name: null,
      initialBid: 0,
      open: null,
      close: null,
      isUsed: false,
      responsible: {
        id: null,
        name: null
      }
    },
    defaultItem: {
      name: null,
      initialBid: 0,
      open: null,
      close: null,
      isUsed: false,
      responsible: {
        id: null,
        name: null
      }
    }
  }),
  computed: {
    formTitle() {
      return this.isEdition === false ? 'Novo leilão' : 'Editar leilão'
    }
  },
  watch: {
    async dialog(val) {
      try {
        const response = await this.$axios.get('v1/User')
        this.users = response.data
      } catch (err) {
        this.showErrorToast(err)
      }
      val || this.close()
    }
  },
  async created() {
    await this.initialize()
  },
  methods: {
    async initialize() {
      try {
        const response = await this.$axios.get('v1/Auction')
        this.auctions = response.data
      } catch (err) {
        this.auctions = []
        this.showErrorToast(err)
      }
      this.loading = false
    },
    showSuccessToast(msg) {
      this.$toast.success(msg, {
        iconPack: 'mdi',
        icon: 'check',
        action: {
          text: 'Fechar',
          onClick: (_, toastObject) => {
            toastObject.goAway(0)
          }
        }
      })
    },
    showErrorToast(err) {
      this.$toast.error(err, {
        iconPack: 'mdi',
        icon: 'alert-circle',
        duration: null,
        action: {
          text: 'Fechar',
          onClick: (_, toastObject) => {
            toastObject.goAway(0)
          }
        }
      })
    },
    isSuccessStatusCode(statusCode) {
      return statusCode > 199 && statusCode < 400
    },
    async editItem(item) {
      this.isEdition = true
      this.editedItem = Object.assign({}, item)
      try {
        const response = await this.$axios.get('v1/User')
        this.users = response.data
      } catch (err) {
        this.showErrorToast(err)
      }
      this.dialog = true
    },
    async deleteItem(item) {
      const isSure = confirm('Tem certeza que quer remover este leilão?')

      if (isSure) {
        await this.delete(item.id)
      }
    },
    close() {
      this.$refs.form.reset()
      this.dialog = false
      this.isEdition = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
      })
    },
    async save() {
      try {
        await this.$axios.post('v1/Auction', {
          name: this.editedItem.name,
          initialBid: this.editedItem.initialBid,
          open: this.editedItem.open,
          close: this.editedItem.close,
          isUsed: this.editedItem.isUsed,
          responsibleUserId: this.editedItem.responsible.id
        })
        await this.initialize()
        this.showSuccessToast('Leilão criado com sucesso')
        this.close()
      } catch (err) {
        this.showErrorToast(err)
      }
    },
    async update() {
      try {
        await this.$axios.put(
          `v1/Auction/${this.editedItem.id}`,
          this.editedItem
        )
        await this.initialize()
        this.showSuccessToast('Leilão atualizado com sucesso')
        this.close()
      } catch (err) {
        this.showErrorToast(err)
      }
    },
    async delete(id) {
      try {
        await this.$axios.delete(`v1/Auction/${id}`)
        await this.initialize()
        this.showSuccessToast('Leilão removido com sucesso')
      } catch (err) {
        this.showErrorToast(err)
      }
    }
  }
}
</script>
