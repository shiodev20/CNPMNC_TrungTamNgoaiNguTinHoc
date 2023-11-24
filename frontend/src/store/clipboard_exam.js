import axios from "axios";
const Clipboard = {
  namespaced: true,
  state: {
    clipboard: null,
  },
  mutations: {
    set_value: (state, value) => {
      state.clipboard = value;
    },
  },
  getters: {
    getbyid: (state) => (id) => {
      return state.exam.filter((e) => e.id == id)[0];
    },
    getbyidcus_content: (state) => (idcus, id_context) => {
      return state.clipboard.filter(
        (e) => e.id_customer == idcus && e.id_content == id_context
      )[0];
    },
    getbyidx: (state) => (idexambatch, id_context, id_customer) => {
      return state.clipboard.filter(
        (e) =>
          e.id_exambatch == idexambatch &&
          e.id_context == id_context &&
          e.id_customer == id_customer
      );
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/clipboard_exambacth");
      commit("set_value", data.data);
    },
    async cretae({ commit }, datad) {
      await axios.post("http://localhost:3000/clipboard_exambacth", datad);
      const data = await axios.get("http://localhost:3000/clipboard_exambacth");
      commit("set_value", data.data);
    },
    async update_ettent({ commit, state }, data) {
      console.log(commit);
      console.log(state);
      await axios.put(
        "http://localhost:3000/clipboard_exambacth/" + data.id,
        data
      );
    },
  },
};
export default Clipboard;
