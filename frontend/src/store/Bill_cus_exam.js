import axios from "axios";
const bill_cus_exam = {
  namespaced: true,
  state: {
    bill_cus_exam: null,
    status_chose: 1,
  },
  mutations: {
    set_value: (state, value) => {
      state.bill_cus_exam = value;
    },
  },
  getters: {
    getbyid: (state) => (idcus, idclass) => {
      return state.cus_class.filter(
        (e) => e.customer_id == idcus && e.class_id == idclass
      )[0];
    },
    getbyids: (state) => (idcus, id_exambactch) => {
      return state.bill_cus_exam.filter(
        (e) => e.id_cus == idcus && e.id_exambactch == id_exambactch
      );
    },
  },
  actions: {
    async add_cus({ commit }, data) {
      console.log(commit);
      await axios.post("http://localhost:3000/bill_cus_exam", data);
    },
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/bill_cus_exam");
      commit("set_value", data.data);
    },
  },
};
export default bill_cus_exam;
